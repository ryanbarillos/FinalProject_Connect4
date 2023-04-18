using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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
    public static class ConnectFour
    {
        //Local Variables
        private static string[,] _GameBoard = new string[6, 7];



        //The board will have any players' coin set
        public static void SetGameBoard()
        {
            //Row of GameBoard
            for (int a = 0; a < _GameBoard.GetLength(0); a++)
            {
                //Column of GameBoard
                for (int b = 0; b < _GameBoard.GetLength(1); b++)
                {
                    _GameBoard[a, b] = "*";
                }
            }
        }



        //Display the board in all its glory
        public static void PrintGameBoard()
        {
            /*
             * REFERENCES for the FUTURE
             * 
             * Printing 2D arrays
             * https://invidious.baczek.me/watch?v=G1kYoPr1Ru8
             * 
             * Clear Console
             * https://www.geeksforgeeks.org/console-clear-method-in-c-sharp/
             */


            //Clear the console
            Console.Clear();
            
            //Header
            Console.WriteLine("Connect Four Board");
            Console.WriteLine("--------------------------------------------");

            //Row of GameBoard
            for (int a = 0; a < _GameBoard.GetLength(0); a++)
            {
                //Enter new line
                Console.WriteLine();

                //Column of GameBoard
                for (int b = 0; b < _GameBoard.GetLength(1); b++)
                {
                    Console.Write(_GameBoard[a, b] + "\t");
                }
            }

            //Print out the column numbers of the board
            Console.WriteLine();
            for (int i = 0; i <= _GameBoard.GetLength(0); i++)
            {
                Console.Write((i + 1).ToString() + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("NOTE: These numbers represent the COLUMN NO.");
            Console.WriteLine("--------------------------------------------");


            //Enter new lines
            Console.WriteLine();
        }
    }





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





    //public class CPU:Player
    //{

    //}





    internal class Program
    {
        static void PlayGame(List<Player> PlayerList, int playerCount)
        {
            /*
             * Connect Four Board
             * A 7x6 grid
             */
            ConnectFour.SetGameBoard();
            ConnectFour.PrintGameBoard();
            Console.Write(PlayerList[0].PlayerName + ", pick a COLUMN NUMBER to place your coin: ");
        }


        static void Main(string[] args)
        {
            //Local Variables
            List<Player> PlayerList = new List<Player>();
            int playerCount = 0;



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
            //Start the game
            PlayGame(PlayerList, playerCount);
        }
    }
}
