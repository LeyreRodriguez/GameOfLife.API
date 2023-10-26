
namespace GameOfLife.Business
{
    public class Board
    {
        private List<Cell> board;
        public int Rows { get; }
        public int Columns { get; }
        public Board(bool[][] InitialBoard)
        {
            Rows = InitialBoard.Length;
            Columns = InitialBoard[0].Length;
            InitializeBoard(InitialBoard);
        }



        public override bool Equals(object? obj)
        {
            Board newBoard = (Board)obj;
            return this.board.SequenceEqual(newBoard.board);
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

        public Cell GetCell(int row, int col)
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
            // Crear una copia de la lista actual para almacenar los nuevos estados
            List<Cell> newBoard = new List<Cell>(board);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    State currentCellState = GetCell(i, j).State;

                    if (currentCellState == State.Alive)
                    {
                        Underpopulation(GetCell(i, j), newBoard);
                        Overpopulation(GetCell(i, j), newBoard);

                    }
                    else
                    {
                        Reproduction(GetCell(i, j), newBoard);
                    }
                }
            }

            // Actualizar el estado del tablero con la nueva generación
            board = newBoard;
        }

        public void Underpopulation(Cell currentCell, List<Cell> newBoard)
        {
            int liveNeighbors = CountLiveNeighbors(currentCell.x, currentCell.y);

            if (currentCell.isAlive() && liveNeighbors < 2)
            {
                newBoard[currentCell.x * Columns + currentCell.y] = new Cell(State.Dead, currentCell.x, currentCell.y);
            }
        }

        public void Overpopulation(Cell currentCell, List<Cell> newBoard)
        {
            int liveNeighbors = CountLiveNeighbors(currentCell.x, currentCell.y);

            if (currentCell.isAlive() && liveNeighbors > 3)
            {
                newBoard[currentCell.x * Columns + currentCell.y] = new Cell(State.Dead, currentCell.x, currentCell.y);
            }
        }

        public void Reproduction(Cell currentCell, List<Cell> newBoard)
        {
            int liveNeighbors = CountLiveNeighbors(currentCell.x, currentCell.y);

            if (liveNeighbors == 3)
            {
                newBoard[currentCell.x * Columns + currentCell.y] = new Cell(State.Alive, currentCell.x, currentCell.y);
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