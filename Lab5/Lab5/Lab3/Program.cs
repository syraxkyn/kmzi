using Lab2;
using Lab2.DocumentReader;
using Lab3.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> deuthAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k',
                'l','m','n','o','p','q','r','s','t','u','v',
                'w','ä','ö','ü','ß'
            };

            int[] keyH = new int[] { 5, 3, 6, 4, 2, 1 };
            char[] keyHword = new char[] { 'a', 'n', 'd', 'r', 'e', 'y' };
            int[] keyV = new int[] { 6, 8, 5, 4, 3, 1 ,7,2};
            char[] keyVword = new char[] { 's', 'h', 'u', 'l', 'a', 'k', 'o','v' };

            List<KeyValuePair<int,char>>keyVertical = new List<KeyValuePair<int, char>>();
            List<KeyValuePair<int, char>> keyHorizontal = new List<KeyValuePair<int, char>>();

            for (int i =0; i < keyV.Length;i++)
            {
                keyVertical.Add(new KeyValuePair<int,char>(keyV[i],keyVword[i]));
            }
            for (int i = 0; i < keyH.Length; i++)
            {
                keyHorizontal.Add(new KeyValuePair<int, char>(keyH[i],keyHword[i]));
            }

            int n = 4; // размерность матрицы
            char[,] matrix = new char[n, n];
            int count = 0; // счетчик заполненных ячеек
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0) // движение вправо
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = deuthAlphabet[count];
                        count++;
                    }
                }
                else // движение влево
                {
                    for (int j = n - 1; j >= 0; j--)
                    {
                        matrix[i, j] = deuthAlphabet[count];
                        count++;
                    }
                }
            }
            // вывод матрицы в консоль
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            try
            {
                //SnakeService.MakeSnake(deuthAlphabet);
                ReplacementService.MakeReplace(deuthAlphabet, keyVertical, keyHorizontal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }


        }
    }
}
