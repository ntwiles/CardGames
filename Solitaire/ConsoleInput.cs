using System;
using System.Collections.Generic;
using System.Text;

namespace Solitaire
{
    public class ConsoleInput
    {
        public int GetStackChoice()
        {
            int chosenStack = 0;
            bool choosingStack = true;

            while (choosingStack)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char keyChar = keyInfo.KeyChar;

                // Escape to quit.
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    chosenStack = -1;
                    choosingStack = false;
                }

                // 0 to 9 to select a stack.
                if (keyChar >= 48 && keyChar <= 57)
                {
                    chosenStack = (int)keyChar - 48;
                    choosingStack = false;
                }
            }

            return chosenStack;
        }
    }
}
