using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MultiPack
{
    //Encryption library for strings
    public class EncryptionLib
    {
        public static string Decrypt(string encryptedString)
        {
            return (encryptedString.Length > 8 ? DecryptString(encryptedString.Substring(8, encryptedString.Length - 8), encryptedString.Substring(0, 8)) : encryptedString);
        }

        public static string Encrypt(string clearString)
        {
            var password = GetRandomString(8);
            return (password + EncryptString(clearString, password));
        }

        // Encrypt a byte array into a byte array using a key and an Initialization Vector - Rijndael algorithm
        private static byte[] EncryptByteArray(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. 
            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            cs.Close();

            // Now get the encrypted data from the MemoryStream.
            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }

        private static string EncryptString(string clearText, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            byte[] encryptedData = EncryptByteArray(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);
        }

        // Decrypt a byte array into a byte array using a key and an IV - Rijndael algorithm
        private static byte[] DecryptByteArray(byte[] cipherData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the decrypted bytes 
            MemoryStream ms = new MemoryStream();

            // Create a symmetric algorithm. TripleDES can be also used
            // TripleDES alg = TripleDES.Create(); 
            Rijndael alg = Rijndael.Create();

            // Now set the key and the IV. 
            alg.Key = Key;
            alg.IV = IV;

            // Create a CryptoStream through which we are going to be pumping our data. 
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);

            // Close the crypto stream (or do FlushFinalBlock). 
            cs.Close();

            // Get the decrypted data from the MemoryStream. 
            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }

        private static string DecryptString(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            byte[] decryptedData = DecryptByteArray(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static string MD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private static string GetRandomString(int size)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
    }
}
