using System;
using System.Collections.Generic;

namespace GameOfLifeTDD
{
    internal class Board
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
            if (otherBoard == null) { return false; }

            if (cells == null && otherBoard.cells == null) { return true; }

            if (cells == null || otherBoard.cells == null) { return false; }

            // Comparamos las longitudes de las filas y columnas
            if (cells.Length != otherBoard.cells.Length || cells[0].Length != otherBoard.cells[0].Length) { return false; }

            // Comparamos los elementos de las matrices
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[0].Length; j++)
                {
                    if (cells[i][j] != otherBoard.cells[i][j])
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
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborRow = row + i;
                    int neighborCol = col + j;
                    if (i == 0 && j == 0) continue;
                    if (neighborRow >= 0 && neighborRow < Rows && neighborCol >= 0 && neighborCol < Columns)
                    {
                        if (GetCell(neighborRow, neighborCol).State == State.Alive)
                        {
                            liveNeighbors++;
                        }
                    }
                }
            }
            return liveNeighbors;
        }
    }
}