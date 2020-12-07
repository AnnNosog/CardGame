using System;
using System.Collections.Generic;
using System.Diagnostics;
using LogerLib;

namespace CardGame
{
    internal class Game
    {
        private List<Karta> _koloda;
        private List<Player> _players;
        private int _quantityPlayrs;
        private const int _quantityCards = 36;
        private List<Player> _resultGame;
        private Stopwatch watch = new Stopwatch();
        private int _quantitySt;

        public Game(int quantity)
        {
            if (quantity < 2 || quantity > 6)
            {
                string textException = "Неверное количество игроков.";
                Loger.WriteLog(textException, nameof(Game), "Exception");
                throw new ArgumentException(textException);
            }

            _koloda = new List<Karta>();
            _players = new List<Player>();

            for (int i = 0; i < quantity; i++)
            {
                _players.Add(new Player($"{i + 1}"));
            }

            _quantityPlayrs = quantity;
            _resultGame = new List<Player>();
            _quantitySt = 0;
        }

        public void Go()
        {
            CreateKoloda(_koloda);
            ShufflingCards(_koloda);
            DrawCards(_koloda, _players);

            bool count = true;

            watch.Start();

            while (count)
            {
                Gameplay(out count);
            }

            watch.Stop();
        }

        private void CreateKoloda(List<Karta> cards)
        {
            for (int i = 0; i < CardType.Suit.Count; i++)
            {
                for (int j = 0; j < CardType.CardTypes.Count; j++)
                {
                    cards.Add(new Karta(CardType.Suit[i], CardType.CardTypes[j]));
                }
            }
        }

        private void ShufflingCards(List<Karta> cards)
        {
            List<Karta> tempKoloda = new List<Karta>();
            Random rand = new Random();
            int sizeKoloda = _quantityCards;

            for (int i = 0; i < cards.Count; i++)
            {
                int buf = rand.Next(sizeKoloda--);
                tempKoloda.Add(cards[buf]);
                cards.RemoveAt(buf);
            }

            cards.AddRange(tempKoloda);
        }

        private void DrawCards(List<Karta> cards, List<Player> players)
        {
            if (_quantityPlayrs == 5)
            {
                cards.RemoveAt(_quantityCards - 1);
            }

            for (int i = 0; i < cards.Count;)
            {
                int count = 0;

                while (count < _quantityPlayrs)
                {
                    players[count++].SetKarts.Add(cards[i++]);
                }
            }
        }

        #region printKoloda
        //public void PrintKoloda()
        //{

        //    for (int i = 0; i < _koloda.Count; i++)
        //    {
        //        Karta karta = _koloda[i];
        //        Console.WriteLine(karta.Type + karta.Suit);
        //    }
        //}
        #endregion

        #region PrintPlaeysCards
        //public void PrintPlaeysCards()
        //{
        //    for (int i = 0; i < _players.Count; i++)
        //    {
        //        _players[i].OutputCards();
        //        Console.WriteLine();
        //    }
        //}
        #endregion

        private void Gameplay(out bool count)
        {
            List<Karta> bufKoloda = new List<Karta>();
            int winPlayer = 0;

            ClearListNull(_players);

            Player win = _players[0];

            if (_players.Count == 1)
            {
                _resultGame.Add(_players[0]);
                GetInfoFinish();
                count = false;
                return;
            }

            for (int i = 0; i < _players.Count; i++)
            {
                bufKoloda.Add(_players[i].SetKarts[0]);
            }

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].SetKarts[0].Type == CardType.CardTypes[0] && win.SetKarts[0].Type == CardType.CardTypes[CardType.CardTypes.Count - 1])
                {
                    win = _players[i];
                    winPlayer = i;
                    continue;
                }
                else if (_players[i].SetKarts[0].Type == CardType.CardTypes[CardType.CardTypes.Count - 1] && win.SetKarts[0].Type == CardType.CardTypes[0])
                {
                    continue;
                }

                if (_players[i] > win)
                {
                    win = _players[i];
                    winPlayer = i;
                }
                else
                {
                    if (i == winPlayer)
                    {
                        continue;
                    }

                    if (_players[i] == win)
                    {
                        if (_players[i].SetKarts.Count == 1 || win.SetKarts.Count == 1)
                        {
                            continue;
                        }
                        else
                        {
                            if (CardType.CardTypes.FindIndex(p => p == _players[i].SetKarts[1].Type) > CardType.CardTypes.FindIndex(p => p == win.SetKarts[1].Type))
                            {
                                if (_players[i].SetKarts[1].Type == CardType.CardTypes[0] && win.SetKarts[1].Type == CardType.CardTypes[CardType.CardTypes.Count - 1])
                                {
                                    bufKoloda.Add(_players[i].SetKarts[1]);
                                    bufKoloda.Add(win.SetKarts[1]);
                                    _players[i].SetKarts.RemoveAt(1);
                                    win.SetKarts.RemoveAt(1);

                                    win = _players[i];
                                    winPlayer = i;
                                    continue;
                                }
                                else if (_players[i].SetKarts[1].Type == CardType.CardTypes[CardType.CardTypes.Count - 1] && win.SetKarts[1].Type == CardType.CardTypes[0])
                                {
                                    bufKoloda.Add(_players[i].SetKarts[1]);
                                    bufKoloda.Add(win.SetKarts[1]);
                                    _players[i].SetKarts.RemoveAt(1);
                                    win.SetKarts.RemoveAt(1);
                                    continue;
                                }

                                bufKoloda.Add(_players[i].SetKarts[1]);
                                bufKoloda.Add(win.SetKarts[1]);
                                _players[i].SetKarts.RemoveAt(1);
                                win.SetKarts.RemoveAt(1);

                                win = _players[i];
                                winPlayer = i;
                            }
                            else
                            {
                                bufKoloda.Add(_players[i].SetKarts[1]);
                                bufKoloda.Add(win.SetKarts[1]);
                                _players[i].SetKarts.RemoveAt(1);
                                win.SetKarts.RemoveAt(1);
                            }
                        }
                    }
                }
                _quantitySt += 1;
            }

            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].SetKarts.RemoveAt(0);
            }

            _players[winPlayer].SetKarts.AddRange(bufKoloda);

            count = true;

            Console.WriteLine("WIN " + win.Name);
        }

        private void GetInfoFinish()
        {
            string winTeaxt = $"Победил {_resultGame[_resultGame.Count - 1].Name}";
            Console.WriteLine(winTeaxt);
            Loger.WriteLog(winTeaxt, nameof(GetInfoFinish), "Test message");

            string quantityStep = $"Всего было сделано ходов: {_quantitySt}";
            Console.WriteLine(quantityStep);
            Loger.WriteLog(quantityStep, nameof(GetInfoFinish), "Test message");

            string timeGame = $"Время игры: {watch.Elapsed}";
            Console.WriteLine(timeGame);
            Loger.WriteLog(timeGame, nameof(GetInfoFinish), "Test message");

            for (int i = 0; i < _resultGame.Count - 1; i++)
            {
                string loseGamer = $"Выбыл игрок {_resultGame[i].Name}. Количество сделанных ходов: {_resultGame[i].QuantityStep}";
                Console.WriteLine(loseGamer);
                Loger.WriteLog(loseGamer, nameof(GetInfoFinish), "Test message");
            }
        }

        private void ClearListNull(List<Player> players)
        {
            int temp = 0;

            while (temp != players.Count)
            {
                temp = 0;

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].SetKarts.Count == 0)
                    {
                        _players[i].QuantityStep = _quantitySt;
                        _resultGame.Add(_players[i]);
                        _players.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        temp++;
                    }
                }
            }
        }

    }
}
