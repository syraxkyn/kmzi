using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 15; // значение a
            int[] x = new int[] { 10007, 20011, 30013, 40009, 50021 }; // значения x
            int[] n = new int[] { 1024, 2048 }; // значения n
            long y; // значение y

            Console.WriteLine("a={0}", a);
            Console.WriteLine("x={0}", string.Join(", ", x));
            Console.WriteLine("n={0}", string.Join(", ", n));
            Console.WriteLine("x\t n\t y\t time(ms)");

            // Вычисление значения y для каждой пары (x, n)
            foreach (int xi in x)
            {
                foreach (int ni in n)
                {
                    Stopwatch sw = Stopwatch.StartNew(); // измерение времени
                    y = (a * xi) % ni;
                    sw.Stop();
                    Console.WriteLine("{0}\t {1}\t {2}\t {3}", xi, ni, y, sw.ElapsedMilliseconds);
                }
            }

            Console.ReadKey();
        }
    }
}