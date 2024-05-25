using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stick_game
{
    public class Stick_game
    {
        public readonly Random randomizer;
        public Player Turn { get; private set; }
        public GameStatus GameStatus { get; private set; }

        public int RemainingSticks { get; private set; }

        public int InitialSticks { get; }

        public event Action<int> MachineMakesToMove;
        public event EventHandler<int> HumanMakesToMove;
        public event Action<Player> EndOfGame;



        public Stick_game(int sticks_in, Player whoMakesFirstMove)
        {
            if (sticks_in < 8 && sticks_in > 60)
                sticks_in = 10;

            randomizer = new Random();
            InitialSticks = sticks_in;
            GameStatus = GameStatus.NotStarted;
            RemainingSticks = InitialSticks;
            Turn = whoMakesFirstMove;
        }
        public void HumanTakes(int sticks)
        {
            if(sticks < 1 || sticks > 3){
                throw new ArgumentException("You can take from 1 to 3 sticks ");
            }

            if(sticks > RemainingSticks)
            {
                throw new ArgumentException($"You can't take more than remaining. Remains {RemainingSticks}");
            }

            TakeSticks(sticks);
        }

        public void Start()
        {
            if (GameStatus == GameStatus.GameOver)
            {
                RemainingSticks = InitialSticks;
            }

            if (GameStatus == GameStatus.InProgress)
            {
                throw new Exception("Game already started");
            }

            GameStatus = GameStatus.InProgress;
            while (GameStatus == GameStatus.InProgress)
            {
                if(Turn == Player.Computer)
                {

                            ComputerMakesMove();
                }

                else if (Turn == Player.Human)
                {
                    HumanMakesMove();
                }
                FireEndOfGameIfRequeied();

                Turn = Turn == Player.Computer ? Player.Human : Player.Computer;
            }
        }

        private void FireEndOfGameIfRequeied()
        {
            if (RemainingSticks == 0)
            {
                GameStatus = GameStatus.GameOver;
                if (EndOfGame != null)

                    EndOfGame(Turn == Player.Computer ? Player.Human : Player.Computer);
                    
            }
        }

        private void HumanMakesMove()
        {
            if (HumanMakesToMove != null)
                HumanMakesToMove(this, RemainingSticks);
        }

        private void ComputerMakesMove()
        {
            int MaxNumber = RemainingSticks >= 3 ? 3 : RemainingSticks;
            int sticks = randomizer.Next(1, MaxNumber);
            TakeSticks(sticks);
            if (MachineMakesToMove != null)
            {
                MachineMakesToMove(sticks);
            }
        }

        private void TakeSticks(int sticks)
        {
            RemainingSticks -= sticks;
                }
    }
}
