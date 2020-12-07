using System;
using System.Collections.Generic;

namespace CardGame
{
    internal class Karta
    {
        private string _suit;
        private string _type;

        public string Suit => _suit;
        public string Type => _type;

        public Karta(string suit, string type)
        {
            _suit = suit;
            _type = type;
        }
    }
}
