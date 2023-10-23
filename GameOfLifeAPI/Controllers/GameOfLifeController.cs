using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GameOfLifeTDD;

namespace GameOfLifeTDD
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        public const string SavePath = "gameoflife.json";
        public GameOfLife game;

        [HttpPost]
        public IActionResult CreateGame([FromBody] List<List<bool>> initialBoard)
        {
            try
            {
                int rows = initialBoard.Count;
                int cols = initialBoard[0].Count;

                bool[][] stateBoard = new bool[rows][];

                for (int i = 0; i < rows; i++)
                {
                    stateBoard[i] = new bool[cols];

                    for (int j = 0; j < cols; j++)
                    {
                        stateBoard[i][j] = initialBoard[i][j];
                    }
                }

                game = new GameOfLife(stateBoard);
                SaveGameToFile(stateBoard);
                return Ok("Game created and saved.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult CalculateNextGeneration()
        {
            LoadGameFromFile();

            if (game == null)
            {
                return BadRequest("Game has not been initialized.");
            }

            game.nextGen(); 

            bool[][] newBoardArray = ConvertToBoolArray(game.GetBoardState());

            SaveGameToFile(newBoardArray);

            return Ok("Next generation calculated and saved.");
        }

        private bool[][] ConvertToBoolArray(List<List<bool>> boardState)
        {
            int rows = boardState.Count;
            int cols = boardState[0].Count;
            bool[][] boolArray = new bool[rows][];

            for (int i = 0; i < rows; i++)
            {
                boolArray[i] = boardState[i].ToArray();
            }

            return boolArray;
        }



    

        public void SaveGameToFile(bool[][] boardState)
        {
            // Convierte bool[][] a List<List<bool>>
            List<List<bool>> listBoardState = new List<List<bool>>();
            foreach (var row in boardState)
            {
                listBoardState.Add(row.ToList());
            }

            var json = JsonSerializer.Serialize(listBoardState);
            System.IO.File.WriteAllText(SavePath, json);
        }


        public void LoadGameFromFile()
        {
            if (System.IO.File.Exists(SavePath))
            {
                try
                {
                    var json = System.IO.File.ReadAllText(SavePath);
                    var boardState = JsonSerializer.Deserialize <List<List<bool>>>(json);

                    if (boardState != null)
                    {
                        // Assuming that `game` is a field or property in your class
                        game = new GameOfLife(ConvertToBoolArray(boardState));
                    }
                    
                }
                catch (JsonException ex)
                {
                    BadRequest("Error loading game state: " + ex.Message);
                }
            }
        }

    }
}
