using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeTDD
{
    public class GameOfLife
    {

        private Board board;

        public GameOfLife(bool[][] bools)
        {
            this.board = new Board(bools);
        }

        public void nextGen()
        {
            board.CalculateNextGeneration();
        }

        public bool Equals(GameOfLife gameOfLife)
        {
            return gameOfLife.board.Equals(this.board);
        }

        public List<List<bool>> GetBoardState()
        {
            
            return board.GetBoardState();
        }


    }
}