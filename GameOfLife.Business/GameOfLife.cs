
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




    }
}