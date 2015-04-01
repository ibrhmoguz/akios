using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace WebFrame.DataType.Common.Cryptography
{

    /// <summary>
    /// Rijndael Algoritması kullanarak verilen string değerleri şifreler veya çözer
    /// </summary>
   public static class Crytography
    {

       
     //private const string teststring="Gumruk Web Portal";
     private const string teststring = "MERKEZIBILGE";

        static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {

            MemoryStream ms = new MemoryStream();


            Rijndael alg = Rijndael.Create();


            alg.Key = Key;
            alg.IV = IV;


            CryptoStream cs = new CryptoStream(ms,
               alg.CreateEncryptor(), CryptoStreamMode.Write);


            cs.Write(clearData, 0, clearData.Length);


            cs.Close();


            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }

       /// <summary>
       /// Verilen string değeri şifreler
       /// </summary>
       /// <param name="clearText">şifrelenecek değer</param>
       /// <returns>Şifrelenmiş veri</returns>
        public static string Encrypt(string clearText)
        {

            byte[] clearBytes =
              System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(teststring,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});


            byte[] encryptedData = Encrypt(clearBytes,
                     pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);

        }



        static byte[] Decrypt(byte[] cipherData,
                                   byte[] Key, byte[] IV)
        {

            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;


            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);


            cs.Write(cipherData, 0, cipherData.Length);


            cs.Close();

            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }


       /// <summary>
       /// Verilen şifrelenmiş string değerin şifresini çözer.  
       /// </summary>
       /// <param name="cipherText">Şifrelenmiş string değer</param>
       /// <returns>Şifresi çözülmüş veri</returns>
        public static string Decrypt(string cipherText)
        {

            byte[] cipherBytes = Convert.FromBase64String(cipherText);


            PasswordDeriveBytes pdb = new PasswordDeriveBytes(teststring,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] decryptedData = Decrypt(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));


            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
    }
}
