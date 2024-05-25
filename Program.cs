using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stick_game
{

    class Program
    {
        static void Main()
        {
           Console.WriteLine("Initial number of sticks:");
            int stick_in = 10;
            try { 
            stick_in = Convert.ToInt32(Console.ReadLine());
            }

            catch
            {
                Console.WriteLine("An error occurs. Try again");
                Main();
            }
            Stick_game stick_game = new Stick_game(stick_in, Player.Human);
            stick_game.MachineMakesToMove += Game_MachineMakesToMove;
            stick_game.HumanMakesToMove += Game_HumanMakesToMove;
            stick_game.EndOfGame += Game_EndOfGame;

            stick_game.Start();

        }

        private static void Game_EndOfGame(Player player)
        {
            Console.WriteLine($"Winner {player}");
            Console.ReadKey();
        }

        private static void Game_HumanMakesToMove(object sender, int remainingSticks)
        {
            Console.WriteLine($"Remaining sticks {remainingSticks}");
            Console.WriteLine($"Take some sticks") ;

            bool TakenCorrectly = false;
                while(!TakenCorrectly)
            {
                if(int.TryParse(Console.ReadLine(), out int takenSticks)){
                    var stick_game = (Stick_game)sender;
                    try
                    {
                        stick_game.HumanTakes(takenSticks);
                        TakenCorrectly = true;
                    }

                    catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }

        private static void Game_MachineMakesToMove(int sticks)
        {
            Console.WriteLine($"Machine took {sticks}");       
        }
    }
}
