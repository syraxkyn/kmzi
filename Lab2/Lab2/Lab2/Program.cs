   using Lab2.DocumentReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const string fileName = "Lab2-1.xls";

            List<char> portugalAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k',
                'l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','é','í','á','ú','ã'
            };

            List<char> bulgarianAlphabet = new List<char>()
            {
                '\u0430','\u0431','\u0432','\u0433',
                '\u0434','\u0435','\u0454','\u0436','\u0437',
                '\u0438','\u0456','\u0457','\u0439','\u043A',
                '\u043B','\u043C','\u043D','\u043E','\u043F',
                '\u0440','\u0441','\u0442','\u0443','\u0444',
                '\u0445','\u0446','\u0447','\u0448','\u0449',
                '\u044C','\u044E','\u044F'
            };
            while (choice != 5)
            {
                Console.Clear();

                Console.WriteLine("Выберите номер задания:\n- 1\n- 2\n- 3\n- 4\n- 5-выйти");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            EntropyChecker portugalChecker = new EntropyChecker(portugalAlphabet, 0, "Португальский");
                            EntropyChecker bulgarianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");

                            string portugalText = portugalChecker.OpenDocument("portugal.txt").ReadToEnd().ToLower();
                            string bulgarianText = bulgarianChecker.OpenDocument("bulgarian1.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            portugalText = regex.Replace(portugalText, "");
                            bulgarianText = regex.Replace(bulgarianText, "");

                            Dictionary<char, int> portugalDict = portugalChecker.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDict = bulgarianChecker.alphabetListToDictionary();

                            portugalChecker.getSymbolsCounts(portugalText, portugalDict);
                            bulgarianChecker.getSymbolsCounts(bulgarianText, bulgarianDict);

                            Dictionary<char, double> chancesPortugal = portugalChecker.getSymbolsChances(portugalText, portugalDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(bulgarianText, bulgarianDict);

                            portugalChecker.computeTextEntropy(chancesPortugal);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);


                            portugalChecker.printAlphabet();
                            portugalChecker.printChances(chancesPortugal);
                            portugalChecker.printAlhabetEntropy();


                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            double sumPortugal = 0;
                            double sumBulgarian = 0;
                            foreach (KeyValuePair<char, double> x in chancesPortugal)
                            {
                                sumPortugal += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для португальского языка: {sumPortugal}");

                            ExcelDocumentCreator<char,double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chancesPortugal, "first", 0);
                            excel.addValuesFromDict(chancesBulgarian, "first", 3);
                            excel.pack.Save();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            EntropyChecker portugalChecker = new EntropyChecker(new List<char>(){ '0', '1' }, 0, "Бинарный код");
                            EntropyChecker bulgarianChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код");

                            string portugalText = portugalChecker.OpenDocument("portugal.txt").ReadToEnd().ToLower();
                            string bulgarianText = portugalChecker.OpenDocument("bulgarian1.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            portugalText = regex.Replace(portugalText, "");
                            bulgarianText = regex.Replace(bulgarianText, "");

                            string binTextPortugal = "";
                            string binTextBulgarian = "";

                            var textChr = Encoding.UTF8.GetBytes(portugalText);
                            foreach (int chr in textChr)
                            {
                                binTextPortugal += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> portugalDict = portugalChecker.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDict = bulgarianChecker.alphabetListToDictionary();

                            portugalChecker.getSymbolsCounts(binTextPortugal, portugalDict);
                            bulgarianChecker.getSymbolsCounts(binTextBulgarian, bulgarianDict);

                            Dictionary<char, double> chancesPortugal = portugalChecker.getSymbolsChances(binTextPortugal, portugalDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(binTextBulgarian, bulgarianDict);

                            portugalChecker.computeTextEntropy(chancesPortugal);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);


                            portugalChecker.printAlphabet();
                            portugalChecker.printChances(chancesPortugal);
                            portugalChecker.printAlhabetEntropy();


                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            double sumPortugal = 0;
                            double sumBulgarian = 0;
                            foreach (KeyValuePair<char, double> x in chancesPortugal)
                            {
                                sumPortugal += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для португальского языка: {sumPortugal}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chancesPortugal, "second", 0);
                            excel.addValuesFromDict(chancesBulgarian, "second", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            EntropyChecker portugalChecker = new EntropyChecker(portugalAlphabet, 0, "Португальский");
                            EntropyChecker bulgarianChecker = new EntropyChecker(bulgarianAlphabet, 0, "Болгарский");
                            EntropyChecker portugalCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (португальский)");
                            EntropyChecker bulgarianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

                            string portugalText = "shulakovandreialexandrovitch";
                            string bulgarianText = "шулаковандреjалександрович";

                            string binTextPortugal = "";
                            string binTextBulgarian = "";

                            var textChr = Encoding.UTF8.GetBytes(portugalText);
                            foreach (int chr in textChr)
                            {
                                binTextPortugal += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> portugalDict = portugalChecker.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDict = bulgarianChecker.alphabetListToDictionary();
                            Dictionary<char, int> portugalDictBin = portugalCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDictBin = bulgarianCheckerBin.alphabetListToDictionary();

                            portugalChecker.getSymbolsCounts(portugalText, portugalDict);
                            bulgarianChecker.getSymbolsCounts(bulgarianText, bulgarianDict);
                            portugalCheckerBin.getSymbolsCounts(binTextPortugal, portugalDictBin);
                            bulgarianCheckerBin.getSymbolsCounts(binTextBulgarian, bulgarianDictBin);

                            Dictionary<char, double> chancesPortugal = portugalChecker.getSymbolsChances(portugalText, portugalDict);
                            Dictionary<char, double> chancesBulgarian = bulgarianChecker.getSymbolsChances(bulgarianText, bulgarianDict);
                            Dictionary<char, double> chancesPortugalBin = portugalCheckerBin.getSymbolsChances(binTextPortugal, portugalDictBin);
                            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.getSymbolsChances(binTextBulgarian, bulgarianDictBin);

                            portugalChecker.computeTextEntropy(chancesPortugal);
                            bulgarianChecker.computeTextEntropy(chancesBulgarian);
                            portugalCheckerBin.computeTextEntropy(chancesPortugalBin);
                            bulgarianCheckerBin.computeTextEntropy(chancesBulgarianBin);


                            portugalChecker.printAlphabet();
                            portugalChecker.printChances(chancesPortugal);
                            portugalChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {portugalChecker.AlphabetName}: {portugalChecker.AlphabetEntropy *portugalText.Length}");

                            bulgarianChecker.printAlphabet();
                            bulgarianChecker.printChances(chancesBulgarian);
                            bulgarianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {bulgarianChecker.AlphabetName}: {bulgarianChecker.AlphabetEntropy * bulgarianText.Length}");

                            portugalCheckerBin.printAlphabet();
                            portugalCheckerBin.printChances(chancesPortugalBin);
                            portugalCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {portugalCheckerBin.AlphabetName}: {portugalCheckerBin.AlphabetEntropy * binTextPortugal.Length}");

                            bulgarianCheckerBin.printAlphabet();
                            bulgarianCheckerBin.printChances(chancesBulgarianBin);
                            bulgarianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.AlphabetEntropy * binTextBulgarian.Length}");


                            double sumPortugal = 0;
                            double sumBulgarian = 0;
                            double sumPortugalBin = 0;
                            double sumBulgarianBin = 0;
                            foreach (KeyValuePair<char, double> x in chancesPortugal)
                            {
                                sumPortugal += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarian)
                            {
                                sumBulgarian += x.Value;
                            }

                            foreach (KeyValuePair<char, double> x in chancesPortugalBin)
                            {
                                sumPortugalBin += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesBulgarianBin)
                            {
                                sumBulgarianBin += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для болгарского языка: {sumBulgarian}");
                            Console.WriteLine($"Сумма шансов для португальского языка: {sumPortugal}");
                            Console.WriteLine($"Сумма шансов для болгарского языка (бинарный): {sumBulgarianBin}");
                            Console.WriteLine($"Сумма шансов для португальского языка (бинарный): {sumPortugalBin}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("third");
                            excel.addValuesFromDict(chancesPortugal, "third", 0);
                            excel.addValuesFromDict(chancesBulgarian, "third", 3);
                            excel.addValuesFromDict(chancesPortugalBin, "third", 5);
                            excel.addValuesFromDict(chancesBulgarianBin, "third", 7);
                            excel.pack.Save();


                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            EntropyChecker portugalCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (португальский)");
                            EntropyChecker bulgarianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (болгарский)");

                            string portugalText = "shulakovandreyalexandrovich";
                            string bulgarianText = "шулаковандрейалександрович";

                            string binTextPortugal = "";
                            string binTextBulgarian = "";

                            var textChr = Encoding.UTF8.GetBytes(portugalText);
                            foreach (int chr in textChr)
                            {
                                binTextPortugal += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(bulgarianText);
                            foreach (int chr in textChr)
                            {
                                binTextBulgarian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> portugalDictBin = portugalCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> bulgarianDictBin = bulgarianCheckerBin.alphabetListToDictionary();

                            portugalCheckerBin.getSymbolsCounts(binTextPortugal, portugalDictBin);
                            bulgarianCheckerBin.getSymbolsCounts(binTextBulgarian, bulgarianDictBin);

                            Dictionary<char, double> chancesPortugalBin = portugalCheckerBin.getSymbolsChances(binTextPortugal, portugalDictBin);
                            Dictionary<char, double> chancesBulgarianBin = bulgarianCheckerBin.getSymbolsChances(binTextBulgarian, bulgarianDictBin);

                            portugalCheckerBin.printAlphabet();
                            portugalCheckerBin.printChances(chancesPortugalBin);
                            portugalCheckerBin.computeTextEntropy(chancesPortugalBin);
                            portugalCheckerBin.printAlhabetEntropy();
                            bulgarianCheckerBin.printAlphabet();
                            bulgarianCheckerBin.printChances(chancesBulgarianBin);
                            bulgarianCheckerBin.computeTextEntropy(chancesBulgarianBin);
                            bulgarianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {portugalCheckerBin.AlphabetName}: {portugalCheckerBin.computeTextEntropyWithError(chancesPortugalBin, 0.1) * binTextPortugal.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {portugalCheckerBin.AlphabetName}: {portugalCheckerBin.computeTextEntropyWithError(chancesPortugalBin, 1) * binTextPortugal.Length}");
                            Console.WriteLine($"Ошибка = 1.0. Количество информации сообщения. Язык - {portugalCheckerBin.AlphabetName}: {portugalCheckerBin.computeTextEntropyWithError(chancesPortugalBin, 0.9999999) * binTextPortugal.Length}");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin,0.1) * binTextBulgarian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 1) * binTextBulgarian.Length}");
                            Console.WriteLine($"Ошибка = 1.0. Количество информации сообщения. Язык - {bulgarianCheckerBin.AlphabetName}: {bulgarianCheckerBin.computeTextEntropyWithError(chancesBulgarianBin, 0.9999999)*0.97 * binTextBulgarian.Length}");


                            Console.ReadKey();

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
