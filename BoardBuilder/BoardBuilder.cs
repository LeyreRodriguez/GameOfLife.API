using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.API.Test
{
    public class BoardBuilder
    {
        private bool[][] board;
      

        public BoardBuilder(int x, int y)
        {
            board = new bool[x][];
            for (int i = 0; i < x; i++)
            {
                board[i] = new bool[y];
            }

        }

        public BoardBuilder SetAliveCell(int x, int y)
        {
            this.board[x][y] = true;
            return this;
        }

        public bool[][] Build()
        {
            return board;
        }
    }
}
