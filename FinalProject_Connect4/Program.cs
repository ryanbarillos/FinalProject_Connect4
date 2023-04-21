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
* Day Finished: 21 Apr 2023
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



        public static bool InsertCoinInGameBoard(int column, char coin)
        {
            //Check if coin is placed
            bool coinPlaced = false;

            //Scan from the bottom-up for a free row
            for (int i = _GameBoard.GetLength(0); --i >= 0;)
            {
                //Place coin
                if (_GameBoard[i, column] == " ")
                {
                    _GameBoard[i, column] = coin.ToString();
                    coinPlaced = true;
                    break;
                }
            }
            return coinPlaced;
        }



        public static bool IsGameBoardFull()
        {
            int emptyTilesLeft = 0;

            //Row of GameBoard
            for (int a = 0; a < _GameBoard.GetLength(0); a++)
            {
                //Column of GameBoard
                for (int b = 0; b < _GameBoard.GetLength(1); b++)
                {
                    /*
                     * Scan for any more empty spaces
                     * Otherwise, game over---it's a draw!
                     * 
                     * All tiles have been occupied
                     */
                    if (_GameBoard[a, b] == " ")
                    {
                        emptyTilesLeft++;
                    }
                }
            }
            if (emptyTilesLeft > 0) return false;
            return true;
        }



        /*
         * Return more than one value in this function, like in Python
         * 
         * https://devblogs.microsoft.com/dotnet/whats-new-in-csharp-7-0/
         * https://stackoverflow.com/a/42926327
         */
        public static (bool patternFound, string patternMessage) FindWinner(char coin)
        {
            //Local Variables
            bool patternFound = false;
            string theCoin = coin.ToString(), patternMessage = "";
            int limitColumn = -1, limitRow = _GameBoard.GetLength(1);


            /*
             * CHECK #1
             * Search for a Horizontal Line "---"
             */
            if (!patternFound)
            {
                for (int a = _GameBoard.GetLength(0) - 1; a > limitColumn; a--)
                {
                    //Scan each row for winning patterns, from left to right
                    for (int b = 3; b < limitRow; ++b)
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
                                        patternMessage = "Horizontal Line '--' DETECTED!";
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
                    for (int a = _GameBoard.GetLength(0) - 4; a > limitColumn; a--)
                    {
                        //Scan each row for winning patterns, from left to right
                        for (int b = 0; b < limitRow; ++b)
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
                                            patternMessage = "Vertical Line '|' DETECTED!";
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
                        for (int a = _GameBoard.GetLength(0) - 4; a > limitColumn; a--)
                        {
                            int rowNo = 3;

                            //Scan each row for winning patterns, from left to right
                            for (int b = rowNo; b < limitRow; ++b)
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
                                                patternMessage = "Diagonal Line '/' DETECTED!";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        /*
                         * CHECK #4
                         * Search for a Forward Back Slash "\\\"
                         * 
                         * NOTE:
                         * a) Reverse "CHECK #4" to get this to work
                         */
                        if (!patternFound)
                        {
                            for (int a = _GameBoard.GetLength(0) - 4; a > limitColumn; a--)
                            {
                                int rowNo = 3;

                                //Scan each row for winning patterns, from left to right
                                for (int b = rowNo; b < limitRow; ++b)
                                {
                                    if (_GameBoard[a, b - 3] == theCoin)
                                    {
                                        if (_GameBoard[a + 1, b - 2] == theCoin)
                                        {
                                            if (_GameBoard[a + 2, b - 1] == theCoin)
                                            {
                                                if (_GameBoard[a + 3, b] == theCoin)
                                                {
                                                    //Bring the news out---a winner is found!
                                                    patternFound = true;
                                                    //Use string literation to print out the back slash
                                                    patternMessage = @"Diagonal Line '\' DETECTED!";
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
            }
            return (patternFound, patternMessage);
        }




        public class Player
        {
            //Each player will have the check mark that allows him/her to make his/her turn
            public bool MyTurn = false;

            /*
             * Each player will have a unique symbol for his/her Connect coin
             * and be able to retreive his/her coin's symbol
             */
            private char _PlayerCoin;
            public void SetPlayerCoin(char symbol)
            {
                _PlayerCoin = symbol;
            }
            public char GetPlayerCoin()
            {
                return _PlayerCoin;
            }

            //Each player will take his/her turn
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

            //Each Player will be able to set a name
            public string PlayerName { get; set; }

            //Player Constructor
            public Player(string pName, char pCoin)
            {
                PlayerName = pName;
                SetPlayerCoin(pCoin);

            }
        }



        public class PlayerReal : Player
        {
            /*
             * NOTE
             * 
             * This refers to an actual human being playing this game
             */
            //Player Constructor
            public PlayerReal(string rpName, char rpCoin) : base(rpName, rpCoin)
            {
                PlayerName = rpName;
                SetPlayerCoin(rpCoin);

            }
        }



        public class CPU : Player
        {
            /*
             * NOTE
             * 
             * CPU refers to "Computer User/Player" AKA Computer-generated Player
             * This notation was first used in Nintendo's "Super Smash Bros. Brawl" on the Wii console
             * 
             * https://www.ssbwiki.com/List_of_abbreviations#General_terms
             */
            //CPU Constructor
            public CPU(string cpuName, char cpuCoin) : base(cpuName, cpuCoin)
            {
                PlayerName = "CPU";
                SetPlayerCoin(cpuCoin);

            }
        }





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


                /*
                 * Settings for Single Player
                 */
                if (playerCount == 1)
                {
                    PlayerList.Add(new PlayerReal("Player 01", '*'));
                    PlayerList.Add(new CPU("CPU", 'O'));
                }
                /*
                 * Settings for Multiplayer
                 */
                else if (playerCount == 2)
                {
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
                            Regex charsReserved = new Regex(@"^[-–—\s]$");   //Whitespace, hypens, en dash, and em dash


                            //Make sure only one char is entered
                            if (getCoin.Length <= 0 || getCoin.Length > 1) Console.Write("Please enter ONLY ONE character: ");

                            //Prohibit use of reserved chars---used for printing the Game Board
                            else if (charsReserved.IsMatch(getCoin)) Console.Write("That is a RESERVED symbol. Choose something else: ");
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
                                            Console.Write("SYMBOL ALREADY TAKEN! Choose something else: ");
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
                        PlayerList.Add(new PlayerReal(name, coin));
                    }
                }

                /*
                 * Start the game
                 */
                PlayGame(PlayerList, playerCount);
            }





            static void PlayGame(List<Player> PlayerList, int playerCount)
            {
                //Prepare Game Board
                ConnectFour.SetGameBoard();

                //Start the game
                bool someoneWon = false,
                    endInDraw = ConnectFour.IsGameBoardFull();
                int winner = 0;

                while (!(someoneWon || endInDraw))
                {
                    //Prepare the players
                    Player.WhoseTurnDecide(PlayerList);
                    int playingNow = Player.WhoseTurnIsIt(PlayerList);
                    int columnNo = -1;
                    bool isPlayerDone = false;
                    string playerCurrent = "Current Player: " + PlayerList[playingNow].PlayerName + "\nCoin: " + PlayerList[playingNow].GetPlayerCoin() + "\n";

                    //Show the board
                    ConnectFour.PrintGameBoard();

                    /*
                     * When "Current Player" is a real person
                     */
                    if (PlayerList[playingNow].GetType().Name.ToString() == "PlayerReal")
                    {
                        //Display current player's info
                        Console.WriteLine(playerCurrent);

                        //Ask player where to place his/her coin
                        Console.Write("Pick a COLUMN NUMBER to place your coin: ");
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
                                    Console.WriteLine(playerCurrent);
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
                                Console.WriteLine(playerCurrent);
                                Console.Write("Column is FULL. Pick another column: ");
                            }
                            else
                            {
                                var pieces = ConnectFour.FindWinner(PlayerList[playingNow].GetPlayerCoin());
                                bool hasSomeoneWon = pieces.Item1,
                                    hasNobodyWon = ConnectFour.IsGameBoardFull();
                                string message = pieces.Item2;

                                if (hasSomeoneWon)
                                {
                                    someoneWon = hasSomeoneWon;
                                    ConnectFour.PrintGameBoard();
                                    Console.WriteLine(message);
                                    winner = playingNow;
                                }
                                else if (hasNobodyWon)
                                {
                                    ConnectFour.PrintGameBoard();
                                    endInDraw = hasNobodyWon;
                                }
                            }
                        } while (!isPlayerDone);
                    }
                    /*
                     * When "Current Player" is a CPU
                     */
                    else if (PlayerList[playingNow].GetType().Name.ToString() == "CPU")
                    {
                        /*
                         * Use a random number generator for CPU to place its coin
                         * https://invidious.baczek.me/watch?v=1gSFd-YVFP8
                         */
                        Random randNo = new Random();
                        do
                        {
                            //Ensure that columnNo is 1-7
                            columnNo = randNo.Next(0, 7);

                            //Place coin in board, when possible
                            isPlayerDone = ConnectFour.InsertCoinInGameBoard(columnNo, PlayerList[playingNow].GetPlayerCoin());

                            //When AI is done, check if it has won
                            if (isPlayerDone)
                            {
                                var pieces = ConnectFour.FindWinner(PlayerList[playingNow].GetPlayerCoin());
                                bool hasSomeoneWon = pieces.Item1,
                                    hasNobodyWon = ConnectFour.IsGameBoardFull();
                                string message = pieces.Item2;

                                if (hasSomeoneWon)
                                {
                                    someoneWon = hasSomeoneWon;
                                    ConnectFour.PrintGameBoard();
                                    Console.WriteLine(message);
                                    winner = playingNow;
                                }
                                else if (hasNobodyWon)
                                {
                                    ConnectFour.PrintGameBoard();
                                    endInDraw = hasNobodyWon;
                                }
                            }
                        } while (!isPlayerDone);
                    }


                    //Check if a player has won
                    if (someoneWon)
                    {
                        Console.WriteLine($"{PlayerList[winner].PlayerName} WINS!");
                    }

                    //Check if the game comes to a standstill
                    else if (endInDraw)
                    {
                        Console.WriteLine("No more EMPTY tiles left!");
                        Console.WriteLine("Game ends in DRAW.");
                    }
                }
            }
        }
    }
}
