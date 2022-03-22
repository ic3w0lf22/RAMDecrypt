using System;
using System.IO;
using System.Security.Cryptography;

namespace RAMDecrypt
{
    class Program
    {
        private static readonly byte[] Entropy = new byte[] { 0x52, 0x4f, 0x42, 0x4c, 0x4f, 0x58, 0x20, 0x41, 0x43, 0x43, 0x4f, 0x55, 0x4e, 0x54, 0x20, 0x4d, 0x41, 0x4e, 0x41, 0x47, 0x45, 0x52, 0x20, 0x7c, 0x20, 0x3a, 0x29, 0x20, 0x7c, 0x20, 0x42, 0x52, 0x4f, 0x55, 0x47, 0x48, 0x54, 0x20, 0x54, 0x4f, 0x20, 0x59, 0x4f, 0x55, 0x20, 0x42, 0x55, 0x59, 0x20, 0x69, 0x63, 0x33, 0x77, 0x30, 0x6c, 0x66 }; // just realized this has a typo 💀

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Drag your AccountData.json file into the application to decrypt/encrypt it.");
                Console.ReadKey();
                return;
            }
        
            string AccountsPath = args[0];

            Console.WriteLine("WARNING: Do NOT send any of these files to anyone as they can use the information stored in these files to steal your accounts, Robux, or even get your accounts termiated from Roblox!\n");

            if (File.Exists(AccountsPath))
            {
                byte[] FB = File.ReadAllBytes(AccountsPath);
                bool Decrypt = FB[0] + FB[1] + FB[2] == 1;

                File.WriteAllBytes(AccountsPath, Decrypt ? ProtectedData.Unprotect(FB, Entropy, DataProtectionScope.CurrentUser) : ProtectedData.Protect(FB, Entropy, DataProtectionScope.CurrentUser));

                Console.WriteLine(Decrypt ? "Decrypted your account data!": "Encrypted your account data!");
            }

            Console.ReadKey();
        }
    }
}