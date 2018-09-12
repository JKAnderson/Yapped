using SoulsFormats;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yapped
{
    public static class Core
    {
        private static byte[] key = Encoding.ASCII.GetBytes("ds3#jn/8_7(rsY9pg55GFN7VFL#+3n/)");

        public static BND4 ReadRegulation(string path)
        {
            return ReadRegulation(path, out _);
        }

        public static BND4 ReadRegulation(string path, out bool encrypted)
        {
            if (DCX.Is(path))
            {
                encrypted = false;
                return BND4.Read(path);
            }
            else
            {
                byte[] enc = File.ReadAllBytes(path);
                byte[] dec = DecryptByteArray(key, enc);
                encrypted = true;
                return BND4.Read(dec);
            }
        }

        public static void WriteRegulation(string path, bool encrypted, BND4 bnd)
        {
            if (!File.Exists(path + ".bak"))
                File.Copy(path, path + ".bak");

            if (encrypted)
            {
                byte[] dec = bnd.Write();
                byte[] enc = EncryptByteArray(key, dec);
                File.WriteAllBytes(path, enc);
            }
            else
            {
                bnd.Write(path);
            }
        }

        private static byte[] DecryptByteArray(byte[] key, byte[] secret)
        {
            byte[] iv = new byte[16];
            byte[] encryptedContent = new byte[secret.Length - 16];

            Buffer.BlockCopy(secret, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(secret, iv.Length, encryptedContent, 0, encryptedContent.Length);

            using (MemoryStream ms = new MemoryStream())
            using (AesManaged cryptor = new AesManaged())
            {
                cryptor.Mode = CipherMode.CBC;
                cryptor.Padding = PaddingMode.None;
                cryptor.KeySize = 256;
                cryptor.BlockSize = 128;

                using (CryptoStream cs = new CryptoStream(ms, cryptor.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedContent, 0, encryptedContent.Length);
                }
                return ms.ToArray();
            }
        }

        private static byte[] EncryptByteArray(byte[] key, byte[] secret)
        {
            using (MemoryStream ms = new MemoryStream())
            using (AesManaged cryptor = new AesManaged())
            {
                cryptor.Mode = CipherMode.CBC;
                cryptor.Padding = PaddingMode.PKCS7;
                cryptor.KeySize = 256;
                cryptor.BlockSize = 128;

                byte[] iv = cryptor.IV;

                using (CryptoStream cs = new CryptoStream(ms, cryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cs.Write(secret, 0, secret.Length);
                }
                byte[] encryptedContent = ms.ToArray();

                byte[] result = new byte[iv.Length + encryptedContent.Length];

                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);

                return result;
            }
        }
    }
}
