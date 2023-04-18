using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/* 
* Final Project, Connect 4
* by Ryan Barillos, 439090
* 
* Date Started: 15 Apr 2023
* Day Finished: 21 Apr 2023 (hopefully)
*/
namespace FinalProject_Connect4
{
    public class Player
    {
        //Each player will have both wins and losses
        public int PlayerWins = 0, PlayerLosses = 0;

        //Each player will have the check mark that allows him/her to make his/her turn
        public bool MyTurn = false;

        //Each player will have a unique symbol for his/her Connect coin
        private char _PlayerCoin;
        public void SetPlayerCoin(char symbol)
        {
            _PlayerCoin = symbol;
        }

        //Each player will be able to retreive his/her coin's symbol
        public char GetPlayerCoin()
        {
            return _PlayerCoin;
        }

        //Each Player will be able to set a name
        public string PlayerName { get; set; }

        //Player Constructor
        public Player(string name, char coin)
        {
            PlayerName = name;
            SetPlayerCoin(coin);

        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            //Local Variables
            List<Player> PlayerList = new List<Player>();
            int playerCount = 0;

            /*
             * Connect Four Board
             * A 7x6 grid
             */
            String[,] ConnectFourBoard =    {
                                            { "*", "*", "*", "*", "*", "*", "*" },
                                            { "*", "*", "*", "*", "*", "*", "*" },
                                            { "*", "*", "*", "*", "*", "*", "*" },
                                            { "*", "*", "*", "*", "*", "*", "*" },
                                            { "*", "*", "*", "*", "*", "*", "*" },
                                            { "*", "*", "*", "*", "*", "*", "*" }
                                            };


            //Welcome Statement
            Console.WriteLine("Welcome to Connect Four!\nBy Ryan Barillos\n\n");

            //Ask how many players will play
            Console.Write("How many players will play (1-2)? ");
            do
            {
                Regex matchPlayerCount = new Regex(@"^[1-2]$");
                string getPlayerCount = Console.ReadLine();

                if (!matchPlayerCount.IsMatch(getPlayerCount))
                {
                    Console.Write("ERROR! Enter either 1 or 2: ");
                }
                else
                {
                    playerCount = Convert.ToInt16(getPlayerCount);
                    break;
                }
            } while (!(playerCount >= 1 && playerCount <= 2));


            //Create our players
            for (int i = 1; i <= playerCount; i++)
            {
                //Assign name to Player
                string name = "Player 0" + i.ToString();

                //Assign coin symbol to Player
                char coin;
                do
                {
                    Console.Write($"\n{name}, set your coin symbol (only ONE CHARACTER): ");
                    string getCoin = Console.ReadLine();

                    //Make sure only one char is entered
                    if (getCoin.Length > 1) Console.WriteLine("Please enter ONLY ONE character");
                    else
                    {
                        //Check if entered symbol hasn't been used by another Player
                        bool notUsed = true;
                        if (PlayerList.Count != 0)
                        {
                            foreach (var player in PlayerList)
                            {
                                if (player.GetPlayerCoin() == Convert.ToChar(getCoin))
                                {
                                    Console.WriteLine("SYMBOL ALREADY TAKEN! Please try again.");
                                    notUsed = false;
                                    break;
                                }
                            }
                        }
                        if (notUsed)
                        {
                            coin = Convert.ToChar(getCoin);
                            break;
                        }
                    }
                } while (true);

                //Create Player object
                PlayerList.Add(new Player(name, coin));
            }
        }
    }
}
