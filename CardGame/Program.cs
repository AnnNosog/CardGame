using System;
using LogerLib;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //File.Create("log.txt");
            //File.Create("Config.ini");

            Game game = new Game(5);
            game.Go();

            Console.ReadKey();
        }
    }
}
