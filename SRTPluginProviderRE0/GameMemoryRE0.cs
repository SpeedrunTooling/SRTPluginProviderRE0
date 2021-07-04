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
        public int PlayerCurrentHealth { get => _playerCurrentHealth; set => _playerCurrentHealth = value; }
        internal int _playerCurrentHealth;

        public int PlayerMaxHealth { get => _playerMaxHealth; set => _playerMaxHealth = value; }
        internal int _playerMaxHealth;

        public int PlayerCurrentHealth2 { get => _playerCurrentHealth2; set => _playerCurrentHealth2 = value; }
        internal int _playerCurrentHealth2;

        public int PlayerMaxHealth2 { get => _playerMaxHealth2; set => _playerMaxHealth2 = value; }
        internal int _playerMaxHealth2;

        public GameStats Stats { get => _stats; set => _stats = value; }
        internal GameStats _stats;

        public GameInventoryEntry PlayerInventory { get => _playerInventory; set => _playerInventory = value; }
        internal GameInventoryEntry _playerInventory;

        public GameInventoryEntry PlayerInventory2 { get => _playerInventory2; set => _playerInventory2 = value; }
        internal GameInventoryEntry _playerInventory2;

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
