using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Infrastructure;
using System.IO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using GameOfLife.API.Controllers;
using GameOfLife.API.Test;

namespace GameOfLife.Business


{
    [TestFixture]
    public class GameOfLifeAPIShould
    {

        private GameOfLifeController gameController;
        private GameOfLifeV2Controller gameControllerv2;
        private GameOfLife game;

        [SetUp]
        public void SetUp()
        {
            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(@"\GameOfLife.API");
            game = new GameOfLife(fileSystemBoardRepository);
            gameController = new GameOfLifeController(game);
            gameControllerv2 = new GameOfLifeV2Controller(game);

        }

        [Test]
        public void CreateGame_ValidInput_ReturnsOk_v1()
        {
            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(0, 1)
            .SetAliveCell(1, 0)
            .SetAliveCell(1, 1)
            .Build();

            // Act
            IActionResult result = gameController.CreateGame(initialBoard);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void CalculateNextGeneration_ValidInput_ReturnsOk_v1()
        {
            string gameId = "40dd2909-89f4-4b48-98e5-55a4c3941461";
            // Act
            IActionResult result = gameController.CalculateNextGeneration(gameId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void CreateGame_ValidInput_ReturnsOk_v2()
        {
            int[][] initialBoard = new int[][]{
                new int[] { 3, 3 },
                new int[] { 0, 1 },
                new int[] { 1, 0 },
                new int[] { 1, 1 }};

            // Act
            IActionResult result = gameControllerv2.CreateGame( initialBoard);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void CalculateNextGeneration_ValidInput_ReturnsOk_v2()
        {
            string gameId = "40dd2909-89f4-4b48-98e5-55a4c3941461";
            // Act
            IActionResult result = gameControllerv2.CalculateNextGeneration(gameId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }


    }


}
