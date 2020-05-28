using System;
using System.Text;

namespace Solitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            SolitaireGame solitaire = new SolitaireGame();

            solitaire.Play();
        }
    }
}
