
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

        public void nextGen()
        {
            this.board = boardRepository.Load();
            board.CalculateNextGeneration();
            boardRepository.Save(board);
        }

        public bool Equals(GameOfLife gameOfLife)
        {
            return gameOfLife.board.Equals(this.board);
        }

        public List<List<bool>> GetBoardState()
        {
            
            return board.GetBoardState();
        }

        public bool[][] ToArray()
        {
            return board.ToArray();
        }

        public void NewGame(bool[][] values)
        {
            board = new Board(values);
            boardRepository.Save(board);
        }


    }
}