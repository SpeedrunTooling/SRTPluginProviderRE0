using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SRTPluginProviderRE0
{
    public class GameMemoryRE0 : IGameMemoryRE0
    {
        private const string IGT_TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss\.fff";
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

        public short Kills { get => _kills; set => _kills = value; }
        internal short _kills;

        public short Shots { get => _shots; set => _shots = value; }
        internal short _shots;

        public short Recoveries { get => _recoveries; set => _recoveries = value; }
        internal short _recoveries;

        public int Saves { get => _saves; set => _saves = value; }
        internal int _saves;

        public float IGT { get => _igt; set => _igt = value; }
        internal float _igt;

        public TimeSpan IGTTimeSpan
        {
            get
            {
                TimeSpan timespanIGT;

                if (IGT >= 0f)
                    timespanIGT = TimeSpan.FromSeconds(IGT);
                else
                    timespanIGT = new TimeSpan();

                return timespanIGT;
            }
        }

        public string IGTFormattedString => IGTTimeSpan.ToString(IGT_TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);
    }
}
