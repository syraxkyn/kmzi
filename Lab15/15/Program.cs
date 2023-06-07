using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа на тему исследование методов текстовой стенографии");
            Console.WriteLine("Модификация апроша;");
            Console.WriteLine("Шифрование:");
            Console.WriteLine("Впишите сообщение:");
            aproshEncryption();
            Console.WriteLine("Расшифрование:");
            aproshDecryption();
        }

        public static void aproshDecryption()
        {
            Document document = new Document("aprosh.docx");
            int lines_count = document.Sections[0].Body.Paragraphs.Count;
            String arr = "";
            int size = 0;
            string containerText = " ";
            string secretMessage = " ";
            int containerLength = containerText.Length;
            int messageLength = secretMessage.Length;
            string steganographicText = "";
            for (int i = 0; i < messageLength; i++)
            {
                char containerChar = containerText[i];
                char messageChar = secretMessage[i];
                int modifiedChar = containerChar + messageChar;

                steganographicText += (char)modifiedChar;
            }
           steganographicText += containerText.Substring(messageLength);
            for (int i = 0; i < lines_count; i++)
            {
                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1 && document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.B == 1)
                {
                    size = i;
                    break;
                }

                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1)
                {
                    arr += '1';
                }
                else
                {
                    arr += '0';
                }
            }


            Console.WriteLine("Полученное сообщение: " + BinaryToString(arr));
            Console.ReadLine();
        }
       

        public static void aproshEncryption()
        {
            Document document = new Document("Text1.docx");
            double lines_count = document.Sections[0].Body.Paragraphs.Count;
            Console.WriteLine(lines_count);
            Console.WriteLine("Впишите сообщение:");
            String data = Console.ReadLine();
            String bin = StringToBinary(data);
            string steganographicText = "";
            // Реализация метода извлечения сообщения из стеганографического текста
            int steganographicLength = steganographicText.Length;

            Paragraph firstParagraph = document.FirstSection.Body.FirstParagraph;
            Random random = new Random();
            foreach (Paragraph paragraph in document.GetChildNodes(NodeType.Paragraph, true))
            {
                // Обход всех текстовых диапазонов в параграфе
                foreach (Run run in paragraph.Runs)
                {
                    // Получение объекта шрифта из текстового диапазона
                    Aspose.Words.Font font = run.Font;
                    
                    int randomValue = random.Next(1, 23);
                    Console.WriteLine(randomValue);
                    font.Kerning = randomValue;
                    // Добавьте дополнительные условия для других текстовых диапазонов

                }
            }

            //// Получение первого текстового диапазона из параграфа
            //Run firstRun = firstParagraph.Runs[0];

            //// Получение объекта шрифта из текстового диапазона
            //Aspose.Words.Font font = firstRun.Font;

            //// Изменение свойств шрифта
            //font.Name = "Arial";
            //font.Size = 12;
            //font.Bold = true;
            //font.Kerning = 2;

            string extractedMessage = "";
            for (int i = 0; i < steganographicLength; i++)
            {
                char steganographicChar = steganographicText[i];
                char originalChar = (char)(steganographicChar - steganographicText[i]);

                extractedMessage += originalChar;
            }

            document.Save("aprosh.docx");
        }

        public static string StringToBinary(string data)
        {
            String sb = "";

            foreach (char c in data.ToCharArray())
            {
                sb += Convert.ToString(c, 2).PadLeft(8, '0');
            }

            while (sb.Length % 8 != 0)
            {
                sb = "0" + sb;
            }

            return sb;
        }

        //Binary to String
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i + 8 - 1 <= data.Length; i += 8){byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));}
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

    }
}
