using GameOfLife.Business;
using Newtonsoft.Json;

namespace GameOfLife.Infrastructure
{
    public class FileSystemBoardRepository : BoardRepository
    {
        private string path;

        public FileSystemBoardRepository(string path)
        {
            this.path = path;
        }

        public Board Load()
        {
            string json = File.ReadAllText(path);
            BoardData boardData = JsonConvert.DeserializeObject<BoardData>(json);

            return boardData.toBoard();
        }

        public void Save(Board board)
        {

            BoardData boardData = board.toDTO();
            string json = JsonConvert.SerializeObject(boardData);

            File.WriteAllText(path, json);

        }

    }


   


}