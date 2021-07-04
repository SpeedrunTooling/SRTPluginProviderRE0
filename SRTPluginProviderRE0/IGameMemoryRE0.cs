using SRTPluginProviderRE0.Structs.GameStructs;
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
        int PlayerCurrentHealth2 { get; set; }
        int PlayerMaxHealth2 { get; set; }

        GameStats Stats { get; set; }

        GameInventoryEntry PlayerInventory { get; set; }
        GameInventoryEntry PlayerInventory2 { get; set; }

        TimeSpan IGTTimeSpan { get; }
        string IGTFormattedString { get; }

    }
}