using System.Runtime.InteropServices;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]

    public unsafe struct GameSaveData
    {
        [FieldOffset(0x8)] public int Money;
        [FieldOffset(0x14)] public short LeonCurrentHP;
        [FieldOffset(0x16)] public short LeonMaxHP;
        [FieldOffset(0x18)] public short AshleyCurrentHP;
        [FieldOffset(0x1A)] public short AshleyMaxHP;
        [FieldOffset(0x36C)] public short IGTFrames;

        public static GameSaveData AsStruct(byte[] data)
        {
            fixed (byte* pb = &data[0])
            {
                return *(GameSaveData*)pb;
            }
        }
    }
}