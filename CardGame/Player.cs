using System;
using System.Collections.Generic;
using LogerLib;

namespace CardGame
{
    internal class Player
    {
        private string _name;
        private List<Karta> _SetKarts;
        private int _quantityStep;

        public string Name => _name;
        public int QuantityStep
        {
            get { return _quantityStep; }
            set { _quantityStep = value; }
        }
        public List<Karta> SetKarts
        {
            get
            {
                return _SetKarts;
            }
            set
            {
                _SetKarts = value;
            }
        }

        public Player(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                string textException = "Некорректное имя";
                Loger.WriteLog(textException, nameof(Player), "Exception");
                throw new ArgumentNullException(textException);
            }

            _name = name;
            _SetKarts = new List<Karta>();
        }

        public void OutputCards()
        {
            for (int i = 0; i < _SetKarts.Count; i++)
            {
                Karta karta = _SetKarts[i];
                Console.WriteLine($"{karta.Type}{karta.Suit}");
            }
        }

        public static bool operator >(Player p1, Player p2)
        {
            return CardType.CardTypes.FindIndex(p => p == p1.SetKarts[0].Type) > CardType.CardTypes.FindIndex(p => p == p2.SetKarts[0].Type);
        }

        public static bool operator <(Player p1, Player p2)
        {

            return CardType.CardTypes.FindIndex(p => p == p1.SetKarts[0].Type) < CardType.CardTypes.FindIndex(p => p == p2.SetKarts[0].Type);
        }

        public static bool operator ==(Player p1, Player p2)
        {

            return CardType.CardTypes.FindIndex(p => p == p1.SetKarts[0].Type) == CardType.CardTypes.FindIndex(p => p == p2.SetKarts[0].Type);
        }

        public static bool operator !=(Player p1, Player p2)
        {

            return CardType.CardTypes.FindIndex(p => p == p1.SetKarts[0].Type) != CardType.CardTypes.FindIndex(p => p == p2.SetKarts[0].Type);
        }
    }
}
