using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SRTPluginProviderRE0.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8)]
    public unsafe struct GameInventoryEntry
    {
        [FieldOffset(0x0)] private int id;
        [FieldOffset(0x4)] private int quantity;
        public int ID => id;
        public string Name => ItemDatabase.ItemNames[ID];
        public int Quantity => quantity;
        public bool Is2Slots => ItemDatabase.Is2Slots.Contains(ID);
        public bool HasQuantity => ItemDatabase.HasQuantity.Contains(ID);
        public bool IsWeapon => ItemDatabase.Weapons.Contains(ID);
        public bool IsItem => ItemDatabase.Items.Contains(ID);
        public bool IsThrowable => ItemDatabase.Throwable.Contains(ID);
    }

    public class ItemDatabase
    {
        public static List<int> HasQuantity = new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10, 11, 14, 17, 19, 22, 23, 32, 33, 34, 35, 36, 37, 38, 39, 40, 55 };

        public static List<int> Weapons = new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10, 11, 17, 19, 22, 23 };

        public static List<int> Items = new List<int>() { 32, 33, 34, 35, 36, 37, 40, 55 };

        public static List<int> Throwable = new List<int>() { 14, 38, 39 };

        public static List<int> Is2Slots = new List<int>() { 5, 6, 7, 8, 9, 11, 23, 104, };

        public static Dictionary<int, string> ItemNames = new Dictionary<int, string>()
        {
            { 0, "Empty Slot" },
            { 2, "Knife" },
            { 3, "Handgun" },
            { 4, "Handgun" },
            { 5, "Hunting gun" },
            { 6, "Shotgun" },
            { 7, "Grenade launcher" },
            { 8, "Grenade launcher" },
            { 9, "Grenade launcher" },
            { 10, "Magnum" },
            { 11, "Sub-Machinegun" },
            { 14, "Molotov Cocktail" },
            { 17, "Custom handgun" },
            { 19, "Custom Handgun" },
            { 22, "Magnum Revolver" },
            { 23, "Rocket Launcher" },
            { 25, "CU-NULL" },
            { 26, "Handgun Parts" },
            { 31, "AM-NULL" },
            { 32, "Handgun Ammo" },
            { 33, "Shotgun Ammo" },
            { 34, "Magnum Ammo" },
            { 35, "Grenades" },
            { 36, "Acid Grenades" },
            { 37, "Napalm Grenades" },
            { 38, "Empty Bottle" },
            { 39, "Gas Tank" },
            { 40, "Machinegun Ammo" },
            { 42, "RE-NULL" },
            { 43, "Green Herb" },
            { 44, "Blue Herb" },
            { 45, "Red Herb" },
            { 46, "Herb Mix (green + green)" },
            { 47, "Herbal Mix (green+ green + green)" },
            { 48, "Herbal Mix (green + red)" },
            { 49, "Herbal Mix (green + blue)" },
            { 50, "Herbal Mix (green + green + blue)" },
            { 51, "Herbal Mix (green + blue + red)" },
            { 52, "Herbal Mix" },
            { 53, "First-Aid spray" },
            { 54, "SA-NULL" },
            { 55, "Ink Ribbon" },
            { 56, "KEY-NULL" },
            { 57, "Gold Ring" },
            { 58, "Silver Ring" },
            { 59, "Briefcase" },
            { 60, "Briefcase" },
            { 61, "Briefcase" },
            { 62, "Briefcase" },
            { 63, "Lighter Fluid" },
            { 65, "Train Key" },
            { 66, "Breeding Rm. Key" },
            { 67, "Elavator Key" },
            { 68, "Facility Key" },
            { 69, "Facility Key" },
            { 70, "Facility Key" },
            { 71, "BL. Leech Charm" },
            { 72, "Gr. Leech Charm" },
            { 73, "Factory Key" },
            { 74, "Factory Key" },
            { 75, "Blue Keycard" },
            { 76, "Keycard" },
            { 77, "Magnetic Card" },
            { 78, "Locker Key" },
            { 79, "Unity tablet" },
            { 80, "Obedience Tablet" },
            { 81, "Discipline Tablet" },
            { 82, "Panel Opener" },
            { 83, "Vice Handle" },
            { 86, "Crank Handle" },
            { 87, "Handle" },
            { 88, "Book of Good" },
            { 89, "Book of Evil" },
            { 90, "Bl. Leech Charm" },
            { 91, "Gr. Leech Charm" },
            { 92, "Input Reg. Coil" },
            { 93, "Output Reg. Coil" },
            { 94, "Battery" },
            { 95, "Hi-Power Battery" },
            { 96, "Sterilizing Agent" },
            { 97, "Motherboard" },
            { 99, "Microfilm A" },
            { 100, "Microfilm B" },
            { 102, "Shaft Key" },
            { 103, "Train Key" },
            { 104, "Hookshot" },
            { 105, "Lighter" },
            { 106, "Lighter" },
            { 107, "Conductor's Key" },
            { 108, "Fire Key" },
            { 110, "Water Key" },
            { 111, "Up Key" },
            { 112, "Down Key" },
            { 113, "Dining Car Key" },
            { 117, "Leech Capsule" },
            { 118, "Dial" },
            { 119, "Duralumin Case" },
            { 120, "Statue of Evil" },
            { 121, "Statue of Good" },
            { 122, "Jewelry Box" },
            { 123, "Mixing Set" },
            { 124, "MO Disk" },
            { 125, "Ice Pick" },
            { 126, "Iron Needle" },
            { 127, "Shaft Key" },
            { 128, "Industrial Water" },
            { 129, "Empty Battery" },
            { 130, "Angel Wings" },
            { 131, "Black Wing" },
            { 132, "White Statue" },
            { 133, "Black Statue" },
            { 134, "Red Chemical" },
            { 135, "Blue Chemical" },
            { 136, "Green Chemical" },
            { 137, "Stripping Agent" },
            { 139, "Sulfuric Acid" },
            { 140, "Battery Fluid" },
            { 141, "Closet Key" },
            { 142, "MA-NULL" },
            { 143, "Train Map" },
            { 144, "Training Facility Map" },
            { 145, "Training Facility Basement Map" },
            { 146, "Lab Map" },
            { 147, "Tratment Plant Map" },
            { 148, "Factory Map" },
            { 149, "FA-NULL" },
            { 150, "Player's Manual 1" },
            { 151, "Player's Manual 2" },
            { 152, "Court Order for Transportation" },
            { 153, "Investigation Orders" },
            { 154, "Notice to Supervisors" },
            { 180, "2nd Slot of Previous Item" }
        };
    }
}
