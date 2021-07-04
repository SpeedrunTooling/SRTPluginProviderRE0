using System;

namespace SRTPluginProviderRE0
{
    public interface IGameMemoryRE0
    {
        // Versioninfo
        string GameName { get; }
        string VersionInfo { get; }

        int PlayerCurrentHealth { get; set; }
        int PlayerMaxHealth { get; set; }

        short Kills { get; set; }

        short Shots { get; set; }

        short Recoveries { get; set; }

        int Saves { get; set; }

        float IGT { get; set; }

        TimeSpan IGTTimeSpan { get; }

        string IGTFormattedString { get; }

    }
}