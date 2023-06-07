using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


/*BBS(вар. 3)
    парам: n=256, p,q – обосновать выбор  (11, 23, n=253)
    n = p*q (при делении p,q на 4 дб остаток 3) - число Блюма
    x - вз. простое с n
    xt = (xo)^2 mod n

    Пр: 	p=11, q=19
	        11mod 4=3, 19mod 4=3
	        n=pq = 11*19 = 209

	        выберем х взаимно простое с n (например 3)
	        xo = 3* mod 209 = 9
	        x1 = 9* mod 209 = 81
	        x2 = 81* mod 209 = 82 …
   ------------------------------------------

    RC4 + оценка скорости генерации  
    n=6, ключ = {1, 11, 21, 31, 41, 51}
*/

namespace lab8
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(RSAgenerator.getRSAres() + "\n");

            //----------- R C 4 ---------------

            Console.WriteLine("\n\n\n ----------- R C 4 ---------------\n");

            int[] ikey = { 20, 21, 22, 23, 60, 61 };
            byte[] key = new byte[ikey.Length];

            for (int i = 0; i < ikey.Length; i++)
            {
                key[i] = Convert.ToByte(ikey[i]);
            }

            RC4 rc = new RC4(key);
            RC4 rc2 = new RC4(key);
            byte[] testBytes = ASCIIEncoding.ASCII.GetBytes("Andrey Shulakov Alexandrovic");

            byte[] encrypted = rc.Encode(testBytes, testBytes.Length);
            Console.WriteLine($"Зашифрованнная строка : {ASCIIEncoding.ASCII.GetString(encrypted)}");         


            byte[] decrypted = rc2.Encode(encrypted, encrypted.Length);
            Console.WriteLine($"Расшифрованнная строка : {ASCIIEncoding.ASCII.GetString(decrypted)}");
            
            Console.ReadKey();
        }
    }
}
