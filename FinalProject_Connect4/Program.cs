using System;
/* 
 * Final Project, Connect 4
 * by Ryan Barillos, 439090
 * 
 * Date Started: 15 Apr 2023
 * Day Finished: 21 Apr 2023 (hopefully)
 */
namespace FinalProject_Connect4
{
    internal class Program
    {
        public class Player
        {
            //Each player will have both wins and losses
            public int  PlayerWins = 0,
                        PlayerLosses = 0;

            //Each player will have the check mark that allows him/her to make his/her turn
            public bool MyTurn = false;

            //Each player will have a unique identifier for his/her Connect coin
            public char PlayerCoin = ' ';
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
