using System;
using System.Collections.Generic;
using System.Text;

namespace RollSpelGrupp6.Classes.UIs
{
    public class Printer
    {
        public void PrintInColor(ConsoleColor consoleColor, string sentence, bool newLine = true)
        {
            Console.ForegroundColor = consoleColor;
            if (newLine)
            {
                Console.WriteLine(sentence);
            }
            else
            {
                Console.Write(sentence);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}