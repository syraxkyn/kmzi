﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Enigma
    {
        private static readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string _rotor8 = "FKQHTLXOCBJSPDZRAMEWNIUYGV";
        private static readonly string _rotor2 = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private static readonly string _betaRotor = "LEYJVCNIXWPBQMDRTAKZGFUHOS";
        private static readonly string[] _reflectorBDunn = { "AF", "BV", "CP", "DJ", "EI", "GO", "HY", "KR", "LZ", "MX", "NW", "TQ", "SU" };

        public string Crypt(string text, int posR, int posM, int posL)
        {
            var rotorR = new Rotor(_rotor8, posR);
           var rotorM = new Rotor(_betaRotor, posM);
          var rotorL = new Rotor(_rotor2, posL);
            var result = new StringBuilder(text.Length);
            char symbol;

            foreach (var ch in text)
            {
               
            
                Console.Write(ch);
                Console.WriteLine();

                if (ch == ' ')
                { 
                    symbol = rotorR[_alphabet.IndexOf('X')];
                }
                symbol = rotorR[_alphabet.IndexOf(ch)-1];
                LogToConsole(symbol);
                symbol = rotorM[_alphabet.IndexOf(symbol)-1];
                LogToConsole(symbol);
                symbol = rotorL[_alphabet.IndexOf(symbol)-1];
                LogToConsole(symbol);
                
                
                symbol = _reflectorBDunn.First(x => x.Contains(symbol)).First(x => !x.Equals(symbol));
                LogToConsole(symbol);
                symbol = _alphabet[rotorL.IndexOf(symbol)+1];
                 LogToConsole(symbol);
                 symbol = _alphabet[rotorM.IndexOf(symbol)+1];
                 LogToConsole(symbol);
                 symbol = _alphabet[rotorR.IndexOf(symbol)+1];
                LogToConsole(symbol);
                //Console.WriteLine();
                result.Append(symbol);
            }

            return result.ToString();
        }

        public void LogToConsole(char symbol)
        {
            Console.Write(" ==> " + symbol);
        }
    }
}
