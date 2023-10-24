
namespace GameOfLife.Business
{
    public class Board
    {
        private List<Cell> board;
        private int Rows { get; }
        private int Columns { get; }
        private bool[][] cells;
        public Board(bool[][] InitialBoard)
        {
            Rows = InitialBoard.Length;
            Columns = InitialBoard[0].Length;
            InitializeBoard(InitialBoard);
        }



        public bool Equals(Board otherBoard)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {

                    if (GetCell(i, j).State != otherBoard.GetCell(i, j).State)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public List<List<bool>> GetBoardState()
        {
            List<List<bool>> boardState = new List<List<bool>>();
            for (int i = 0; i < Rows; i++)
            {
                List<bool> row = new List<bool>();
                for (int j = 0; j < Columns; j++)
                {
                    row.Add(GetCell(i, j).State == State.Alive);
                }
                boardState.Add(row);
            }
            return boardState;
        }

        public bool[][] ToArray()
        {
            bool[][] values = new bool[Rows][];

            for (int i = 0; i < Rows; i++)
            {
                values[i] = new bool[Columns];
                for (int j = 0; j < Columns; j++)
                {
                    values[i][j] = false;
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (GetCell(i, j).State == State.Alive)
                    {
                        values[i][j] = true;
                    }
                }
            }

            return values;
        }


        private void InitializeBoard(bool[][] InitialBoard)
        {
            board = new List<Cell>();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    State cellState = InitialBoard[i][j] ? State.Alive : State.Dead;
                    board.Add(new Cell(cellState, i, j));
                }
            }
        }

        private Cell GetCell(int row, int col)
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Columns)
            {
                return board[row * Columns + col];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid row or column index.");
            }
        }

        public void CalculateNextGeneration()
        {
            var newBoard = new bool[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                newBoard[i] = new bool[Columns];
                for (int j = 0; j < Columns; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(i, j);

                    if (GetCell(i, j).State == State.Alive)
                    {
                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            newBoard[i][j] = false;
                        }
                        else
                        {
                            newBoard[i][j] = true;
                        }
                    }
                    else
                    {
                        if (liveNeighbors == 3)
                        {
                            newBoard[i][j] = true;
                        }
                        else
                        {
                            newBoard[i][j] = false;
                        }
                    }
                }
            }

            // Actualizar el estado del tablero con la nueva generación
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GetCell(i, j).UpdateState(newBoard[i][j] ? State.Alive : State.Dead);
                }
            }
        }

        private int CountLiveNeighbors(int row, int col)
        {
            int liveNeighbors = 0;
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int neighborRow = row + dr[i];
                int neighborCol = col + dc[i];

                if (neighborRow >= 0 && neighborRow < Rows && neighborCol >= 0 && neighborCol < Columns)
                {
                    if (GetCell(neighborRow, neighborCol).isAlive())
                    {
                        liveNeighbors++;
                    }
                }
            }
            return liveNeighbors;

        }
    }
}