using System.Runtime.InteropServices;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x1034)]

    public struct GameEnemy
    {
        [FieldOffset(0x1030)] private int currentHP;
        public int CurrentHP => currentHP;
        public bool IsAlive => CurrentHP > 0;
    }
}