using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameOfLifeTDD.Tests
{
    [TestFixture]
    public class GameOfLifeControllerTests
    {
        private GameOfLifeController controller;

        [SetUp]
        public void Setup()
        {
            controller = new GameOfLifeController();
        }

        [Test]
        public void CreateGame_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var initialBoard = new List<List<bool>>
            {
                new List<bool> { true, false, true },
                new List<bool> { false, true, false },
                new List<bool> { true, false, true }
            };

            // Act
            var result = controller.CreateGame(initialBoard) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Game created and saved.", result.Value);
        }

      

        [Test]
        public void CalculateNextGeneration_GameInitialized_ReturnsOkResult()
        {
            // Arrange
            var initialBoard = new List<List<bool>>
            {
                new List<bool> { true, false, true },
                new List<bool> { false, true, false },
                new List<bool> { true, false, true }
            };
            controller.CreateGame(initialBoard); // Initialize the game

            // Act
            var result = controller.CalculateNextGeneration() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Next generation calculated and saved.", result.Value);
        }

       

        [Test]
        public void LoadGameFromFile_GameSaved_ReturnsNotNullGame()
        {
            // Arrange
            var initialBoard = new bool[][]
                {
                    new bool[] { true, false, true },
                    new bool[] { false, true, false },
                    new bool[] { true, false, true }
                };

            controller.SaveGameToFile(initialBoard);

            // Act
            controller.LoadGameFromFile();

            // Assert
            Assert.IsNotNull(controller.game);
        }

    }
}
