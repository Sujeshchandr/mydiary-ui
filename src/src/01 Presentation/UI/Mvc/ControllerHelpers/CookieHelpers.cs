using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyDiary.UI.ControllerHelpers
{
    public static class CookieHelper
    {
        public static string PassPhrase
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["passPhrase"];
            }
        }

        public static class CookieKeys
        {
            public const string UserId = "UserId";
            public const string CookieName = "Diary";
        }

        #region SET

        public static void SetCookie(int userId, int cookieExpireDate = 30)
        {
            HttpCookie myCookie = new HttpCookie(CookieKeys.CookieName);
            myCookie.Value = userId.ToString().Encrypt(PassPhrase);
            myCookie.Expires = DateTime.Now.AddDays(cookieExpireDate);
            myCookie.HttpOnly = true;//Cannot be accessed by client side script 
            HttpContext.Current.Response.Cookies.Add(myCookie);
             
        }

        public static string GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName].Value;

        }

        public static string GetDecryptedDiaryCookieValue()
        {
            try
            {

                string encryptedCookieValue = GetCookie(CookieKeys.CookieName);
                if (string.IsNullOrEmpty(encryptedCookieValue)) return null;
                return encryptedCookieValue.Decrypt(PassPhrase);
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion


    }

    public static class StringCipher
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(this string plainText, string passPhrase)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(this string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}