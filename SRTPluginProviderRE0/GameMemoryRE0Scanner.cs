﻿using ProcessMemory;
using SRTPluginProviderRE0.Structs.GameStructs;
using System;
using System.Diagnostics;
using Windows.Win32;

namespace SRTPluginProviderRE0
{
    internal unsafe class GameMemoryRE0Scanner : IDisposable
    {
        private readonly int MAX_ITEMS = 6;
        private readonly int MAX_ENTITES = 32;

        // Variables
        private ProcessMemoryHandler memoryAccess;
        private GameMemoryRE0 gameMemoryValues;
        public bool HasScanned;
        public bool ProcessRunning => memoryAccess is not null && memoryAccess.ProcessRunning;
        public uint ProcessExitCode => (memoryAccess is not null) ? memoryAccess.ProcessExitCode : 0U;

        // Pointer Address Variables
        private nint pointerAddressHP;
        private nint pointerAddressStats;
        private nint pointerAddressInventory;
        private nint pointerAddressEnemy;

        // Pointer Classes
        private FreeLibrarySafeHandle BaseAddress { get; set; }

        private MultilevelPointer[] PointerEnemy { get; set; }
        private MultilevelPointer[] PointerHP { get; set; }
        private MultilevelPointer PointerStats { get; set; }
        private MultilevelPointer PointerInventory { get; set; }

        private GamePlayer EmptyPlayer = new GamePlayer();
        private GameEnemy EmptyEnemy = new GameEnemy();

        internal GameMemoryRE0Scanner(Process process = null)
        {
            gameMemoryValues = new GameMemoryRE0();
            if (process != null)
                Initialize(process);
        }

        internal unsafe void Initialize(Process process)
        {
            if (process is null)
                return; // Do not continue if this is null.

            if (!SelectPointerAddresses(GameHashes.DetectVersion(process.MainModule.FileName)))
                return; // Unknown version.

            uint pid = GetProcessId(process).Value;
            memoryAccess = new ProcessMemoryHandler(pid);
            if (ProcessRunning)
            {
                BaseAddress = new FreeLibrarySafeHandle(process.MainModule.BaseAddress, false);
                // Broken, might be missing something for 32-bit support. Access violation error upon execution of this line.
                //BaseAddress = NativeWrappers.GetProcessBaseAddress(pid, PInvoke.ListModules.LIST_MODULES_32BIT); // Bypass .NET's managed solution for getting this and attempt to get this info ourselves via PInvoke since some users are getting 299 PARTIAL COPY when they seemingly shouldn't.
                {
                    nint baseAddressNIntPtr = BaseAddress.DangerousGetHandle();

                    //POINTERS
                    var position = 0;
                    PointerHP = new MultilevelPointer[2];
                    for (var i = 0; i < PointerHP.Length; i++)
                    {
                        position = (i * 0x4) + 0x11C;
                        PointerHP[i] = new MultilevelPointer(memoryAccess, (nint*)(baseAddressNIntPtr + pointerAddressHP), position, 0x30);
                    }

                    PointerEnemy = new MultilevelPointer[MAX_ENTITES];
                    for (var i = 0; i < MAX_ENTITES; i++)
                    {
                        position = (i * 0x10) + 0x2C;
                        PointerEnemy[i] = new MultilevelPointer(memoryAccess, (nint*)(baseAddressNIntPtr + pointerAddressEnemy), position);
                    }

                    PointerStats = new MultilevelPointer(memoryAccess, (nint*)(baseAddressNIntPtr + pointerAddressStats));

                    PointerInventory = new MultilevelPointer(memoryAccess, (nint*)(baseAddressNIntPtr + pointerAddressInventory));

                    gameMemoryValues._playerInventory = new GameInventoryEntry[MAX_ITEMS];
                    gameMemoryValues._playerInventory2 = new GameInventoryEntry[MAX_ITEMS];
                    gameMemoryValues._enemyHealth = new GameEnemy[MAX_ENTITES];
                }
            }
        }

        private bool SelectPointerAddresses(GameVersion version)
        {
            switch (version)
            {
                case GameVersion.RE0WW_20250317_1:
                    {
                        pointerAddressEnemy = 0x9CC0D0;
                        pointerAddressHP = 0xA2D434;
                        pointerAddressStats = 0x9CBE9C;
                        pointerAddressInventory = 0x9CBF44;
                        return true;
                    }
                case GameVersion.RE0WW_20210702_1:
                    {
                        pointerAddressEnemy = 0x9CE0D0;
                        pointerAddressHP = 0xA2F414;
                        pointerAddressStats = 0x9CDE9C;
                        pointerAddressInventory = 0x9CDF44; // Player1 0x24 +0x4 - 0x4C Player2 0x64 +0x4 - 0x8C
                        return true;
                    }
            }

            // If we made it this far... rest in pepperonis. We have failed to detect any of the correct versions we support and have no idea what pointer addresses to use. Bail out.
            return false;
        }

        internal void UpdatePointers()
        {
            for (var i = 0; i < PointerHP.Length; i++)
            {
                PointerHP[i].UpdatePointers();
            }
            PointerStats.UpdatePointers();
            PointerInventory.UpdatePointers();
            for (var i = 0; i < MAX_ENTITES; i++)
            {
                PointerEnemy[i].UpdatePointers();
            }
        }

        internal unsafe IGameMemoryRE0 Refresh()
        {
            // Rebecca
            gameMemoryValues._player = PointerHP[0].Deref<GamePlayer>(0x0);

            // Billy and Wesker
            if (PointerHP[0].Address != PointerHP[1].Address)
                gameMemoryValues._player2 = PointerHP[1].Deref<GamePlayer>(0x0);
            else
                gameMemoryValues._player2 = EmptyPlayer;

            // Game Statistics
            gameMemoryValues._stats = PointerStats.Deref<GameStats>(0x0);

            //Inventory 1
            for (var i = 0; i < MAX_ITEMS; i++)
                gameMemoryValues._playerInventory[i] = PointerInventory.Deref<GameInventoryEntry>((i * 0x8) + 0x24);

            gameMemoryValues._currentPersonal = PointerInventory.Deref<GameInventoryEntry>(0x54);

            gameMemoryValues._equippedSlot = PointerInventory.DerefInt(0x5C);

            //Inventory 2
            for (var i = 0; i < MAX_ITEMS; i++)
                gameMemoryValues._playerInventory2[i] = PointerInventory.Deref<GameInventoryEntry>((i * 0x8) + 0x64);

            gameMemoryValues._currentPersonal2 = PointerInventory.Deref<GameInventoryEntry>(0x94);

            gameMemoryValues._equippedSlot2 = PointerInventory.DerefInt(0x9C);

            for (var i = 0; i < MAX_ENTITES; i++)
            {
                //var entityName = PointerEnemy[i].DerefUnicodeString(0x174, 32);
                //if (entityName.StartsWith("uPlayer", StringComparison.InvariantCultureIgnoreCase)) gameMemoryValues._enemyHealth[i] = EmptyEnemy;
                try
                {
                    if (PointerHP[0].Address != PointerEnemy[i].Address && PointerHP[1].Address != PointerEnemy[i].Address)
                    {
                        gameMemoryValues._enemyHealth[i] = PointerEnemy[i].Deref<GameEnemy>(0x0);
                    }
                    else
                    {
                        gameMemoryValues._enemyHealth[i] = EmptyEnemy;
                    }
                }
                catch
                {
                    gameMemoryValues._enemyHealth[i] = EmptyEnemy;
                }
            }

            HasScanned = true;
            return gameMemoryValues;
        }

        private uint? GetProcessId(Process process) => (uint?)process?.Id;

        private unsafe bool SafeReadByteArray(nuint address, nuint size, out byte[] readBytes)
        {
            readBytes = new byte[size];
            fixed (byte* p = readBytes)
                return memoryAccess.TryGetByteArrayAt(address, size, p);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (memoryAccess != null)
                        memoryAccess.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~REmake1Memory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}