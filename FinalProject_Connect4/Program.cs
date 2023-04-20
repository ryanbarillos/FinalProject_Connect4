using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
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
                    _GameBoard[a, b] = " ";
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
                Console.Write("-" + "\t");
            }
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



        public static bool InsertCoinInGameBoard(int row, char coin)
        {
            //Check if coin is placed
            bool coinPlaced = false;

            //Scan from the bottom-up for a free row
            for (int i = _GameBoard.GetLength(0); --i >= 0;)
            {
                //Place coin
                if (_GameBoard[i, row] == " ")
                {
                    _GameBoard[i, row] = coin.ToString();
                    coinPlaced = true;
                    break;
                }
            }
            return coinPlaced;
        }



        /*
         * Return more than one value in this function, like one can do in Python
         * 
         * https://devblogs.microsoft.com/dotnet/whats-new-in-csharp-7-0/
         * https://stackoverflow.com/a/42926327
         */
        public static (bool patternFound, string patternMessage) FindWinner(char coin)
        {
            //Local Variables
            bool patternFound = false;
            string theCoin = coin.ToString(), patternMessage = "";


            if (!patternFound)
            {
                /*
                 * CHECK #1
                 * Search for a Horizontal Line "---"
                 */
                for (int a = _GameBoard.GetLength(0) - 1; a > 0; a--)
                {
                    int rowNo = 3;

                    //Scan each row for winning patterns, from left to right
                    for (int b = rowNo; b < _GameBoard.GetLength(1); ++b)
                    {
                        if (_GameBoard[a, b] == theCoin)
                        {
                            if (_GameBoard[a, b - 1] == theCoin)
                            {
                                if (_GameBoard[a, b - 2] == theCoin)
                                {
                                    if (_GameBoard[a, b - 3] == theCoin)
                                    {
                                        //Bring the news out---a winner is found!
                                        patternFound = true;
                                        patternMessage = "Horizontal Line DETECTED!";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                /*
                 * CHECK #2
                 * Search for a Vertical Line "|||"
                 */
                if (!patternFound)
                {
                    for (int a = _GameBoard.GetLength(0) - 4; a > 0; a--)
                    {
                        //Scan each row for winning patterns, from left to right
                        for (int b = 0; b < _GameBoard.GetLength(1); ++b)
                        {
                            if (_GameBoard[a, b] == theCoin)
                            {
                                if (_GameBoard[a + 1, b] == theCoin)
                                {
                                    if (_GameBoard[a + 2, b] == theCoin)
                                    {
                                        if (_GameBoard[a + 3, b] == theCoin)
                                        {
                                            //Bring the news out---a winner is found!
                                            patternFound = true;
                                            patternMessage = "Vertical Line DETECTED!";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    /*
                     * CHECK #3
                     * Search for a Forward Slash "///"
                     * 
                     * NOTE:
                     * a) Combine both search functions of Horizontal Line "---" & Vertical Line "|||"
                     */
                    if (!patternFound)
                    {
                        for (int a = _GameBoard.GetLength(0) - 4; a > 0; a--)
                        {
                            int rowNo = 3;

                            //Scan each row for winning patterns, from left to right
                            for (int b = rowNo; b < _GameBoard.GetLength(1); ++b)
                            {
                                if (_GameBoard[a, b] == theCoin)
                                {
                                    if (_GameBoard[a + 1, b - 1] == theCoin)
                                    {
                                        if (_GameBoard[a + 2, b - 2] == theCoin)
                                        {
                                            if (_GameBoard[a + 3, b - 3] == theCoin)
                                            {
                                                //Bring the news out---a winner is found!
                                                patternFound = true;
                                                patternMessage = "Diagonal Line (Forward Slash) DETECTED!";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (patternFound, patternMessage);
        }





        public class Player
        {
            //Each player will have both wins and losses
            public int PlayerWins = 0, PlayerLosses = 0;

            //Each player will have the check mark that allows him/her to make his/her turn
            public bool MyTurn = false;


            //Each player will have an indicator on if he/she wins or not
            private bool _HasWon = false;
            public bool VictoryStatus()
            {
                return _HasWon;
            }


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

            //Each player will take their turns
            public static void WhoseTurnDecide(List<Player> PlayerList)
            {
                /*
                 * At the VERY START of the game, make 1st player be the first to play
                 * Since, at initilaization of Player object, its "bool MyTurn" = false
                 * Thus, both players will have same values
                 */
                if (PlayerList[0].MyTurn == PlayerList[1].MyTurn)
                {
                    PlayerList[0].MyTurn = true;
                    PlayerList[1].MyTurn = false;
                }
                //During gameplay, alternate turns
                else
                {
                    //Player 01's Turn
                    if (!PlayerList[0].MyTurn)
                    {
                        PlayerList[0].MyTurn = true;
                        PlayerList[1].MyTurn = false;
                    }
                    //Player 02's Turn
                    else if (!PlayerList[1].MyTurn)
                    {
                        PlayerList[0].MyTurn = false;
                        PlayerList[1].MyTurn = true;
                    }
                }
            }

            //Identify whose turn is it
            public static int WhoseTurnIsIt(List<Player> PlayerList)
            {
                //Player 01's Turn
                if (PlayerList[0].MyTurn) return 0;

                //Player 02's Turn
                return 1;
            }

            //Identify who won the game
            public static void PlayerHasWon(Player Player)
            {
                if (!Player.VictoryStatus())
                {
                    Player._HasWon = true;
                }
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

                    Console.Write($"\n{name}, set your coin symbol (only ONE CHARACTER): ");
                    do
                    {
                        string getCoin = Console.ReadLine();

                        //Make sure only one char is entered
                        if (getCoin.Length > 1) Console.Write("Please enter ONLY ONE character: ");
                        else if (getCoin == " ") Console.Write("Whitespace is a RESERVED char. Choose something else: ");
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





            static void PlayGame(List<Player> PlayerList, int playerCount)
            {
                //Prepare Game Board
                ConnectFour.SetGameBoard();

                //Start the game
                bool someoneWon = false;
                while (!someoneWon)
                {
                    //Prepare the players
                    Player.WhoseTurnDecide(PlayerList);
                    int playingNow = Player.WhoseTurnIsIt(PlayerList);

                    //Some local variables
                    int columnNo = -1;
                    bool isPlayerDone = false;

                    //Show the board
                    ConnectFour.PrintGameBoard();

                    //Ask player where to place his/her coin
                    Console.Write(PlayerList[playingNow].PlayerName + ", pick a COLUMN NUMBER to place your coin: ");

                    do
                    {
                        //Ensure that columnNo is 1-7
                        do
                        {
                            Regex matchColumnNo = new Regex(@"^[1-7]$");
                            string getColumnNo = Console.ReadLine();

                            if (!(matchColumnNo.IsMatch(getColumnNo)))
                            {
                                ConnectFour.PrintGameBoard();
                                Console.Write("ERROR! Enter from 1 to 7: ");
                            }
                            else
                            {
                                //Get proper index, from 0 to 6
                                columnNo = Convert.ToInt16(getColumnNo) - 1;
                            }
                        } while (!(columnNo >= 0 && columnNo <= 6));


                        //Place coin in board, when possible
                        isPlayerDone = ConnectFour.InsertCoinInGameBoard(columnNo, PlayerList[playingNow].GetPlayerCoin());


                        //When FULL, force player to pick another column
                        if (!isPlayerDone)
                        {
                            ConnectFour.PrintGameBoard();
                            Console.Write("Column is FULL. Pick another column: ");
                        }
                        else
                        {
                            var pieces = ConnectFour.FindWinner(PlayerList[playingNow].GetPlayerCoin());
                            bool hasSomeoneWon = pieces.Item1;
                            string message = pieces.Item2;

                            if (hasSomeoneWon)
                            {
                                someoneWon = hasSomeoneWon;
                                Console.WriteLine(message);
                            }
                        }
                    } while (!isPlayerDone);
                }
            }
        }
    }
}
