using Lab2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
//y ≡ x + k mod N, 
//х ≡ у – k mod N
//Трисемиус - enigma
namespace Lab4
{
    class Program
    {
        private static readonly char[] codeSequence = "abcdefghijklmnopqrstuvwxyzäöüß".ToCharArray();
        public static string Encryption(string s)
        {
            string result = "";
            if (s.Length % 2 != 0) s += "Я"; // дополняем до чётного количества букв
            char[] c = s.ToCharArray();
            // подменяем недостающие буквы
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 'Й') c[i] = 'И';
                if (c[i] == 'Ё') c[i] = 'Е';
            }
            for (int i = 0; i < c.Length; i += 2)
            {
                int idx = Array.IndexOf(codeSequence, c[i]) * codeSequence.Length + Array.IndexOf(codeSequence, c[i + 1]) + 1;
                result += $"{idx:000}";
            }
            return result;
        }
        public static string Decryption(string s)
        {
            string result = "";
            for (int i = 0; i < s.Length / 3; i++)
            {
                int idx = int.Parse(s.Substring(i * 3, 3)) - 1;
                result += codeSequence[idx / codeSequence.Length].ToString() + codeSequence[idx % codeSequence.Length].ToString();
            }
            return result;
        }
        static void Main(string[] args)
        {
            string germanAlph = "abcdefghijklmnopqrstuvwxyzäöüß";
            string keyWord = "Shulakov";
            const string fileName = "Lab4-1.xls";
            int k = 7;
            EntropyChecker germanChecker = new EntropyChecker(germanAlph, 0, "Немецкий");
            string germanText = germanChecker.OpenDocument("german.txt").ReadToEnd().ToLower();
            Regex regex = new Regex(@"\W");
            germanText = regex.Replace(germanText, "");
            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));

            int c = 0;
            while (true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1- шифр Виженера, ключевое слово – Shulakov");
                Console.WriteLine("2- шифр Порты");
                Console.WriteLine("3- выход");

                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch (c)
                {
                    case 1:
                        {

                            Stopwatch first = new Stopwatch();

                            Console.WriteLine($"Текст для шифрования:\n{germanText}");

                            Dictionary<char, int> alphCounts = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(germanText, alphCounts);

                            Dictionary<char, double> chances = germanChecker.getSymbolsChances(germanText, alphCounts);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 0);
                            var cipher = new VigenereCipher(germanAlph);
                            var inputText = germanText;
                            var password = keyWord;
                            first.Start();
                            var encryptedText = cipher.Encrypt(inputText, password);
                            first.Stop();
                            Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
                            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n");
                            first.Start();
                            string decryptedText = cipher.Decrypt(encryptedText, password);
                            first.Stop();
                            Console.WriteLine("Расшифрованное сообщение: {0}", decryptedText);
                            Console.WriteLine($"Время расшифрования: {first.ElapsedMilliseconds} мс \n");


                            Dictionary<char, int> alphCountsEnc = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(encryptedText, alphCountsEnc);

                            chances = germanChecker.getSymbolsChances(germanText, alphCountsEnc);
                            germanChecker.printChances(chances);

                            Dictionary<char, double>  chances_enc = germanChecker.getSymbolsChances(encryptedText, alphCountsEnc);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 3);
                            excel.pack.Save();

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances_enc, "first", 6);
                            excel.pack.Save();

                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }

                    case 2:
                        {
                            Stopwatch first = new Stopwatch();
                            Console.WriteLine($"Исходная строка: '{germanText}'");
                            first.Start();
                            string crypt = Encryption(germanText);
                            first.Stop();
                            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n");
                            Console.WriteLine($"Зашифрованная строка: {crypt}");
                            first.Start();
                            Console.WriteLine($"Расшифрованная строка: '{Decryption(crypt)}'");
                            first.Stop();
                            Console.WriteLine($"Время расшифрования: {first.ElapsedMilliseconds} мс \n");
                            Console.ReadLine();

                            Dictionary<char, int> alphCounts = germanChecker.alphabetListToDictionary();
                            germanChecker.getSymbolsCounts(germanText, alphCounts);

                            Dictionary<char, double> chances = germanChecker.getSymbolsChances(germanText, alphCounts);
                            germanChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 9);

                            //Dictionary<char, int> alphCountsEnc = germanChecker.alphabetListToDictionary();
                            ////germanChecker.getSymbolsCounts(crypt, alphCountsEnc);

                            //chances = germanChecker.getSymbolsChances(germanText, alphCountsEnc);
                            ////germanChecker.printChances(chances);

                            //excel.createWorksheet("first");
                            //excel.addValuesFromDict(chances, "first", 9);
                            excel.pack.Save();

                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 3:
                        {
                            return;
                        }
                    case -1:
                        {
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
