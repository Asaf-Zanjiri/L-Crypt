using System;
using System.IO;
using System.Security.Cryptography;
using static L_Crypter.Tools;
using System.Text;
using System.Threading;

namespace L_Crypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("L-Crypter | Asaf Zanjiri");
            Console.Write("Specify Encryption and Compression iterations: ");
            int iterations = int.Parse(Console.ReadLine());

            Console.Write("Please drop in your file: ");

            // Gets file data.
            string filePath = @"" + Console.ReadLine().Trim('"');
            Console.WriteLine("[*] Reading Data...");
            byte[] fileData = File.ReadAllBytes(filePath);


            // Encrypting file data.
            byte[] data = fileData;
            byte[] compressedData;
            string encryptionKey = GenerateAESKey(); // Generates AES Key.
            for (int i = 0; i < iterations; i++)
            {
                compressedData = Compress(data);                // Compressing data.
                data = Encrypt(compressedData, encryptionKey);  // Encrypts compressed data.
            }

            // Converting encrypted file data to base64 payload.
            byte[] encryptedCompressedData = data;
            string b64EncryptedCompressedData = Convert.ToBase64String(encryptedCompressedData); // base64 the compressed data

            // Building the stub.
            Console.WriteLine("[*] Building Stub...");
            BuildStub(b64EncryptedCompressedData, encryptionKey, iterations);

        }

    }
}
