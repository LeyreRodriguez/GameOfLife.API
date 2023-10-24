using NUnit.Framework;
using Moq;
using GameOfLife.Business;
using GameOfLife.Infrastructure;
using Newtonsoft.Json;
using FluentAssertions;
using GameOfLife.API.Test;

namespace GameOfLife.Tests

{
    
    [TestFixture]
    public class FileSystemShould
    {
        public string path;
        [SetUp]
        public void SetUp()
        {
            path = "gameoflife.json";
        }
        [Test]
        public void Load_ValidPath_ReturnsBoard()
        {
            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);

            bool[][] initialBoard = new BoardBuilder(5, 5)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .SetAliveCell(2, 3)
            .SetAliveCell(3, 2)
            .Build();

            Board board = new Board(initialBoard);
            fileSystemBoardRepository.Save(board);

            string file = File.ReadAllText(path);

            BoardData json = JsonConvert.DeserializeObject<BoardData>(file);
            json.Should().BeEquivalentTo(board.toDTO());

        }

        [Test]
        public void Save_ValidBoard_SavesToFile()
        {
            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);

            bool[][] initialBoard = new BoardBuilder(5, 5)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .SetAliveCell(2, 3)
            .SetAliveCell(3, 2)
            .Build();

            Board board = new Board(initialBoard);
            fileSystemBoardRepository.Save(board);

            Board boardLoaded = fileSystemBoardRepository.Load();
            Assert.IsTrue(board.Equals(boardLoaded));
        }


  
    }
}
