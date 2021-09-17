namespace BillsManagement.Security
{
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.Utility;
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordCipher
    {
        private const string Secret = "3319FBA22A224DC8833A83B769037D53";

        // Save password secret for each user and for decrypt use the saved secret (Different for each user)

        public static string Encrypt(string password)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Secret);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(password);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static void Decrypt(string password, string requestedPassword)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(password);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Secret);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            var decryptedPassword = streamReader.ReadToEnd();

                            if (decryptedPassword != requestedPassword)
                            {
                                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, GlobalConstants.UnauthorizedMessage);
                            }
                        }
                    }
                }
            }
        }
    }
}
