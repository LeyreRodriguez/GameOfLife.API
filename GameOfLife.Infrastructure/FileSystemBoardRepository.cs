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

        public Board Load(string id)
        {

            string fileName = Path.Combine("JSON", $"{id}.json");
            string json = File.ReadAllText(fileName);
            BoardData boardData = JsonConvert.DeserializeObject<BoardData>(json);

            return boardData.toBoard();
        }

        public void Save(Board board, string id)
        {
            string fileName = Path.Combine("JSON", $"{id}.json");


           // string fileName = $"{id}.json";
            BoardData boardData = board.toDTO();
            string json = JsonConvert.SerializeObject(boardData);

            File.WriteAllText(fileName, json);

        }


    }


   


}