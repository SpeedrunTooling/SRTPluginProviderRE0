using SRTPluginProviderRE0.Structs.GameStructs;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SRTPluginProviderRE0
{
    public class GameMemoryRE0 : IGameMemoryRE0
    {
        private const string IGT_TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss";
        public string GameName => "RE0";

        // Versioninfo
        public string VersionInfo => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        // Leon Stats
        public GamePlayer Player { get => _player; set => _player = value; }
        internal GamePlayer _player;

        public string PlayerName => string.Format("{0}: ", Player.PlayerCharacter.ToString());

        public GamePlayer Player2 { get => _player2; set => _player2 = value; }
        internal GamePlayer _player2;

        public string PlayerName2 => string.Format("{0}: ", Player2.PlayerCharacter.ToString());

        public GameStats Stats { get => _stats; set => _stats = value; }
        internal GameStats _stats;

        public GameInventoryEntry[] PlayerInventory { get => _playerInventory; set => _playerInventory = value; }
        internal GameInventoryEntry[] _playerInventory;

        public int EquippedSlot { get => _equippedSlot; set => _equippedSlot = value; }
        public int _equippedSlot;

        public GameInventoryEntry CurrentPersonal { get => _currentPersonal; set => _currentPersonal = value; }
        internal GameInventoryEntry _currentPersonal;

        public GameInventoryEntry CurrentWeapon
        {
            get
            {
                if (EquippedSlot != -1)
                    return PlayerInventory[EquippedSlot];
                else
                    return _emptyInventory;
            }
        }
        internal GameInventoryEntry _emptyInventory = new GameInventoryEntry();

        public GameInventoryEntry[] PlayerInventory2 { get => _playerInventory2; set => _playerInventory2 = value; }
        internal GameInventoryEntry[] _playerInventory2;

        public int EquippedSlot2 { get => _equippedSlot2; set => _equippedSlot2 = value; }
        public int _equippedSlot2;

        public GameInventoryEntry CurrentPersonal2 { get => _currentPersonal2; set => _currentPersonal2 = value; }
        internal GameInventoryEntry _currentPersonal2;

        public GameInventoryEntry CurrentWeapon2 => PlayerInventory2[EquippedSlot2];

        public GameEnemy[] EnemyHealth { get => _enemyHealth; set => _enemyHealth = value; }
        internal GameEnemy[] _enemyHealth;

        public TimeSpan IGTTimeSpan
        {
            get
            {
                TimeSpan timespanIGT;

                if (Stats.IGT >= 0f)
                    timespanIGT = TimeSpan.FromSeconds(Stats.IGT / 30);
                else
                    timespanIGT = new TimeSpan();

                return timespanIGT;
            }
        }

        public string IGTFormattedString => IGTTimeSpan.ToString(IGT_TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);
    }
}
