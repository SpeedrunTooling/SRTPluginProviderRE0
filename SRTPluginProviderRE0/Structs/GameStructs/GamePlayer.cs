using System.Runtime.InteropServices;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x1034)]

    public struct GamePlayer
    {
        [FieldOffset(0xFC)] private int id;
        [FieldOffset(0x1030)] private int currentHP;

        public CharacterEnumeration PlayerCharacter => (CharacterEnumeration)id;
        public int CurrentHP => currentHP;
        public int MaxHP => PlayerCharacter == CharacterEnumeration.Rebecca ? 150 : 250;
        public float Percentage => CurrentHP > 0 ? (float)CurrentHP / (float)MaxHP : 0f;
        public bool IsAlive => CurrentHP != 0 && MaxHP != 0 && CurrentHP > 0 && CurrentHP <= MaxHP;
        public PlayerState HealthState
        {
            get =>
                !IsAlive ? PlayerState.Dead :
                Percentage >= 0.75f ? PlayerState.Fine :
                Percentage >= 0.50f ? PlayerState.FineToo :
                Percentage >= 0.25f ? PlayerState.Caution :
                PlayerState.Danger;
        }

        public string CurrentHealthState => HealthState.ToString();
    }

    public enum PlayerState
    {
        Dead,
        Fine,
        FineToo,
        Caution,
        Danger
    }
}