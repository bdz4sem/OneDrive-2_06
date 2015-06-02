using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace OneDrive
{
    static class ODSecurity
    {
        public static string guid;
        public static string hash;
        public static byte[] Key;
        public static byte[] IV;
        public static void Create()
        {
            guid = System.Guid.NewGuid().ToString().Replace("-","");
        } // Generates new ODSecurity
        public static void Save(string password)
        {
            FileStream fs = new FileStream("Security.ods", FileMode.Create);
            string temp = guid + GetSha256(password);
            fs.Write(Encoding.ASCII.GetBytes(temp), 0, temp.Length);
            fs.Close();
        }   // Saves guid + sha256(password)
        public static void Open(string password)
        {
            FileStream fs = new FileStream("Security.ods", FileMode.Open);
            byte[] buffer = new byte[64];
            fs.Read(buffer, 0, buffer.Length);
            string temp = Encoding.ASCII.GetString(buffer);
            ODSecurity.guid = temp.Substring(0, 32);
            ODSecurity.hash = temp.Substring(32, 32);
            if (IsCorrectPass(password))
            {
                Key = Encoding.ASCII.GetBytes(GetSha384(password).Substring(0, 32));
                IV = Encoding.ASCII.GetBytes(GetSha384(password).Substring(32, 16));
            }
            else
            {
                guid = null;
                hash = null;
            }
            fs.Close();
        }   // guid = null in case of wrong pass
        public static string GetSha384(string input)
        {
            System.Security.Cryptography.SHA384 sha1 = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            return Encoding.ASCII.GetString(sha1.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }   // returns sha384-hash of input
        public static string GetSha256(string input)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            return Encoding.ASCII.GetString(sha256.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }   // returns sha256-hash of input
        public static bool IsCorrectPass(string password)
        {
            if (GetSha256(password) == hash)
                return true;
            return false;
        }  
        public static bool IsODSCreated()
        {
            string curFile = @"Security.ods";
            if (File.Exists(curFile)) return true;
            return false;
        }   // Checks, whether ODS is created
    }
}
