using System.Runtime.InteropServices;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 80)]

    public struct GameStats
    {
        [FieldOffset(0x38)] private int saves;
        [FieldOffset(0x3C)] private float igt;
        [FieldOffset(0x4A)] private short kills;
        [FieldOffset(0x4C)] private short shots;
        [FieldOffset(0x4E)] private short recoveries;
        public int Saves => saves;
        public float IGT => igt;
        public short Kills => kills;
        public short Shots => shots;
        public short Recoveries => recoveries;

    }
}