﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sha256_salting
{
    public class SHA256
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"\nХеширование SHA256\n");
            long OldTicks = DateTime.Now.Ticks;
            string text = "Shulakov Andrey Alexandrovich";
            string salt = CreateSalt(15);
            string hash = GenerateSHA256(text, salt);

            Console.WriteLine("Message:  " + text + "\nСоль: " + salt + "\nХэш:  " + hash);
            //Console.WriteLine("{0:x2}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");
            Console.ReadKey();
        }

        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateSHA256(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ToHex(hash);
        }

        public static string ToHex(byte[] ba)
        {

            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach(byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
