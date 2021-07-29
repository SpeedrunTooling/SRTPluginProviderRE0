using SRTPluginProviderRE0.Structs.GameStructs;
using System;

namespace SRTPluginProviderRE0
{
    public interface IGameMemoryRE0
    {
        // Versioninfo
        string GameName { get; }
        string VersionInfo { get; }

        GamePlayer Player { get; set; }
        GamePlayer Player2 { get; set; }

        GameStats Stats { get; set; }

        GameInventoryEntry[] PlayerInventory { get; set; }
        GameInventoryEntry[] PlayerInventory2 { get; set; }
        GameEnemy[] EnemyHealth { get; set; }

        TimeSpan IGTTimeSpan { get; }
        string IGTFormattedString { get; }

    }
}