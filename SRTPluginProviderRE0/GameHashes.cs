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
        private static readonly byte[] re0ww_20210702_1 = new byte[32] { 0x04, 0x2A, 0x3F, 0x46, 0x83, 0x71, 0xDB, 0x68, 0x67, 0x72, 0x82, 0xEE, 0xA7, 0x95, 0xBD, 0x07, 0x51, 0x06, 0x9E, 0x5B, 0x0D, 0x71, 0x14, 0x64, 0xC7, 0xEB, 0x5E, 0x54, 0x5A, 0x5F, 0x40, 0x60 };
        public static GameVersion DetectVersion(string filePath)
        {
            byte[] checksum;
            using (SHA256 hashFunc = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                checksum = hashFunc.ComputeHash(fs);

            if (checksum.SequenceEqual(re0ww_20210702_1))
            {
                Console.WriteLine("Steam Version Detected");
                return GameVersion.RE0WW_20210702_1;
            }

            Console.WriteLine("Unknown Version");
            return GameVersion.Unknown;
        }
    }
}
