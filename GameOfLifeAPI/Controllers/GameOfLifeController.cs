using Microsoft.AspNetCore.Mvc;
using GameOfLife.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameOfLife.Business
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        public const string SavePath = "gameoflife.json";
        private GameOfLife game;

        public GameOfLifeController(GameOfLife game)
        {
            this.game = game;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateGame([FromBody] bool[][] initialBoard)
        {
            game.NewGame(initialBoard);
            return Ok("Game created and saved.");
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CalculateNextGeneration()
        {
            game.nextGen();
            return Ok("Game update with new generation.");
        }



    }
}
