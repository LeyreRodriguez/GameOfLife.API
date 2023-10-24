﻿using GameOfLife.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Infrastructure;
using System.IO;
using GameOfLife.API.Test;

namespace GameOfLife.Business


{
    [TestFixture]
    public class GameOfLifeAPIShould
    {

        private GameOfLifeController gameController;
        private GameOfLife game;

        [SetUp]
        public void SetUp()
        {
            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository("gameoflife.json");
            game = new GameOfLife(fileSystemBoardRepository);
            gameController = new GameOfLifeController(game);

        }

        [Test]
        public void CreateGame_ValidInput_ReturnsOk()
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
            Assert.AreEqual("Game created and saved.", okResult.Value);
        }

        [Test]
        public void CalculateNextGeneration_ValidInput_ReturnsOk()
        {
            string gameId = "abc123";
            // Act
            IActionResult result = gameController.CalculateNextGeneration(gameId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Game update with new generation.", okResult.Value);
        }



    }


}
