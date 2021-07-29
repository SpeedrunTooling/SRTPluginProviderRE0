using System.Runtime.InteropServices;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x1034)]

    public struct GameEnemy
    {
        [FieldOffset(0x1030)] private uint currentHP;
        public uint CurrentHP => currentHP;
        //public uint MaxHP => PlayerCharacter == CharacterEnumeration.Rebecca ? 150 : 250;
        //public float Percentage => CurrentHP > 0 ? (float)CurrentHP / (float)MaxHP: 0f;
        public bool IsAlive => CurrentHP != 0xFFFFFFFF && CurrentHP > 0;
    }
}