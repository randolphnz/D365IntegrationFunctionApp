using D365IntegrationFunctionApp.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.Services
{
    internal class DataEncryptService : IDataEncryptService
    {
        public void EncryptCsvFile(string inputFilePath, string outputFilePath, string password)
        {
            byte[] key, iv;
            GenerateKeyAndIV(password, out key, out iv);

            using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
        }

        private static void GenerateKeyAndIV(string password, out byte[] key, out byte[] iv)
        {
            using (var sha256 = SHA256.Create())
            {
                key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            using (var md5 = MD5.Create())
            {
                iv = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
