using GameOfLife.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure
{
    public static class BoardMapper
    {
        public static BoardData toDTO(this Board board)
        {
            var cells = new List<CellData>();
            var array = board.ToArray();
            for (int i = 0; i < array[0].Length; i++)
            {
                for (int j = 0; j < array[1].Length; j++)
                {
                    cells.Add(new CellData() { State = array[i][j], x = i, y = j });
                }
            }
            return new BoardData() { Cells = cells };


        }

        public static Board toBoard(this BoardData boardData)
        {
            IEnumerable<CellData> cells = boardData.Cells;

            int maxX = 0, maxY = 0;
            foreach (CellData cell in cells)
            {
                if (cell.x > maxX) maxX = cell.x;
                if (cell.y > maxY) maxY = cell.y;
            }

            bool[][] values = new bool[maxX + 1][];
            for (int i = 0; i <= maxX; i++)
            {
                values[i] = new bool[maxY + 1];
                for (int j = 0; j <= maxY; j++)
                {
                    values[i][j] = false;
                }
            }

            foreach (CellData cell in cells)
            {
                int x = cell.x;
                int y = cell.y;
                bool state = cell.State;

                values[x][y] = state;
            }

            return new Board(values);
        }

    }
}
