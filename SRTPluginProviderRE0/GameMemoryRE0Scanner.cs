using ProcessMemory;
using System;
using System.Diagnostics;

namespace SRTPluginProviderRE0
{
    internal unsafe class GameMemoryRE0Scanner : IDisposable
    {

        // Variables
        private ProcessMemoryHandler memoryAccess;
        private GameMemoryRE0 gameMemoryValues;
        public bool HasScanned;
        public bool ProcessRunning => memoryAccess != null && memoryAccess.ProcessRunning;
        public int ProcessExitCode => (memoryAccess != null) ? memoryAccess.ProcessExitCode : 0;

        // Pointer Address Variables
        private int pointerAddressHP;
        private int pointerAddressStats;
        private int pointerAddressInventory;

        // Pointer Classes
        private IntPtr BaseAddress { get; set; }

        private MultilevelPointer[] PointerHP { get; set; }
        private MultilevelPointer PointerStats { get; set; }

        internal GameMemoryRE0Scanner(Process process = null)
        {
            gameMemoryValues = new GameMemoryRE0();
            if (process != null)
                Initialize(process);
        }

        internal unsafe void Initialize(Process process)
        {
            if (process == null)
                return; // Do not continue if this is null.

            SelectPointerAddresses();

            int pid = GetProcessId(process).Value;
            memoryAccess = new ProcessMemoryHandler(pid);
            if (ProcessRunning)
            {
                BaseAddress = NativeWrappers.GetProcessBaseAddress(pid, PInvoke.ListModules.LIST_MODULES_32BIT); // Bypass .NET's managed solution for getting this and attempt to get this info ourselves via PInvoke since some users are getting 299 PARTIAL COPY when they seemingly shouldn't.
                //POINTERS
                var position = 0;
                if (PointerHP == null) PointerHP = new MultilevelPointer[2];

                for (var i = 0; i < PointerHP.Length; i++)
                {
                    position = (i * 0xC) + 0x4;
                    PointerHP[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressHP), 0x6CC, position);
                }
                
                PointerStats = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressStats));
            }
        }

        private void SelectPointerAddresses()
        {
            pointerAddressHP = 0xA2F414;
            pointerAddressStats = 0x9CDE9C;
            pointerAddressInventory = 0x9CDF44; // Player1 0x24 +0x4 - 0x4C Player2 0x64 +0x4 - 0x8C
        }

        internal void UpdatePointers()
        {
            for (var i = 0; i < PointerHP.Length; i++)
            {
                PointerHP[i].UpdatePointers();
            }
            PointerStats.UpdatePointers();
        }

        internal unsafe IGameMemoryRE0 Refresh()
        {
            bool success;

            // Leon
            fixed (int* p = &gameMemoryValues._playerCurrentHealth)
                success = PointerHP[0].TryDerefInt(0x1030, p);
            
            gameMemoryValues._playerMaxHealth = 150;

            fixed (int* p = &gameMemoryValues._playerCurrentHealth2)
                success = PointerHP[1].TryDerefInt(0x1030, p);

            gameMemoryValues._playerMaxHealth2 = 150;

            fixed (int* p = &gameMemoryValues._saves)
                success = PointerStats.TryDerefInt(0x38, p);

            fixed (float* p = &gameMemoryValues._igt)
                success = PointerStats.TryDerefFloat(0x3C, p);

            fixed (short* p = &gameMemoryValues._kills)
                success = PointerStats.TryDerefShort(0x4A, p);

            fixed (short* p = &gameMemoryValues._shots)
                success = PointerStats.TryDerefShort(0x4C, p);

            fixed (short* p = &gameMemoryValues._recoveries)
                success = PointerStats.TryDerefShort(0x4E, p);

            HasScanned = true;
            return gameMemoryValues;
        }

        private int? GetProcessId(Process process) => process?.Id;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        private unsafe bool SafeReadByteArray(IntPtr address, int size, out byte[] readBytes)
        {
            readBytes = new byte[size];
            fixed (byte* p = readBytes)
            {
                return memoryAccess.TryGetByteArrayAt(address, size, p);
            }
        }

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