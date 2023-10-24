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
        private GameOfLife game;
        private string generateId;

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
            generateId = Guid.NewGuid().ToString();
            game.NewGame(initialBoard,generateId);
            var response = new
            {
                Message = "Game created and saved.",
                GameId = generateId
            };

            return Ok(response);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CalculateNextGeneration(string id)
        {
            string fileName = $"game_{id}.json";
            game.nextGen(id);
            return Ok("Game update with new generation.");
        }



    }
}
