using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SRTPluginProviderRE0
{
    /// <summary>
    /// SHA256 hashes for the RE5/BIO5 game executables.
    /// </summary>
    public static class GameHashes
    {
        private static readonly byte[] re0ww_20250317_1 = new byte[32] { 0x67, 0x41, 0x8C, 0x3A, 0x30, 0x7B, 0x49, 0xE9, 0xBD, 0xE8, 0x12, 0x3D, 0xF4, 0xCA, 0xD1, 0xFA, 0x91, 0x0F, 0x3A, 0x29, 0xAD, 0xC3, 0xE4, 0xC0, 0xBA, 0x64, 0xEB, 0x96, 0x52, 0x4B, 0xC1, 0x4B };
        private static readonly byte[] re0ww_20210702_1 = new byte[32] { 0x04, 0x2A, 0x3F, 0x46, 0x83, 0x71, 0xDB, 0x68, 0x67, 0x72, 0x82, 0xEE, 0xA7, 0x95, 0xBD, 0x07, 0x51, 0x06, 0x9E, 0x5B, 0x0D, 0x71, 0x14, 0x64, 0xC7, 0xEB, 0x5E, 0x54, 0x5A, 0x5F, 0x40, 0x60 };
        public static GameVersion DetectVersion(string filePath)
        {
            byte[] checksum;
            using (SHA256 hashFunc = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                checksum = hashFunc.ComputeHash(fs);

            if (checksum.SequenceEqual(re0ww_20250317_1))
            {
                Console.WriteLine($"Steam Version Detected: {GameVersion.RE0WW_20250317_1}");
                return GameVersion.RE0WW_20250317_1;
            }
            else if (checksum.SequenceEqual(re0ww_20210702_1))
            {
                Console.WriteLine($"Steam Version Detected: {GameVersion.RE0WW_20210702_1}");
                return GameVersion.RE0WW_20210702_1;
            }
            else
            {
                Console.WriteLine("Unknown Version");
                return GameVersion.Unknown;
            }
        }
    }
}
