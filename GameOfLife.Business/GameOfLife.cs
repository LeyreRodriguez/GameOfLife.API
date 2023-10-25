
namespace GameOfLife.Business
{
    public class GameOfLife
    {

        private Board board;
        private readonly BoardRepository boardRepository;
        

        public GameOfLife(BoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
        }

        public void nextGen(string gameId)
        {
            this.board = boardRepository.Load(gameId);
            board.CalculateNextGeneration();
            boardRepository.Save(board,gameId);
        }

        public bool Equals(GameOfLife gameOfLife)
        {
            return gameOfLife.board.Equals(this.board);
        }


        public void NewGame(bool[][] values, string gameId)
        {
            board = new Board(values);
            boardRepository.Save(board, gameId);
        }

        public void NewGame(int[][] values, string gameId)
        {
            // Convierte los valores int a bool
            bool[][] boolValues = new bool[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                boolValues[i] = new bool[values[i].Length];
                for (int j = 0; j < values[i].Length; j++)
                {
                    boolValues[i][j] = values[i][j] != 0; // Convierte 0 a false y otros valores a true
                }
            }

            NewGame(boolValues, gameId);
        }



    }
}