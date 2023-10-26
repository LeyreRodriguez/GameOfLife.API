using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using Asp.Versioning;

namespace GameOfLife.Business
{
    [ApiVersion("1.0")]
    [Route("api/v1/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        private GameOfLife game;
        private string generateId;

        public GameOfLifeController(GameOfLife game)
        {
            this.game = game;
        }

        /// <summary>
        /// Return actual generation of GameOfLife and save in a JSON File.
        /// </summary>
        /// <response code="201">Returns the newly created game id</response>
        /// <response code="400">The input values are not valid</response>
        /// 



        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]


        public IActionResult CreateGame([FromBody] bool[][] initialBoard)
        {
            generateId = Guid.NewGuid().ToString();
            game.NewGame(initialBoard, generateId);
            var response = new
            {
                Message = "Game created and saved with 2.0 API version",
                GameId = generateId
            };

            return Ok(response);
        }

        // Cambiar el "Example Value" para el código de respuesta 200 OK

        /// <summary>
        /// Return next generation of GameOfLife from a JSON File and save it.
        /// </summary>
        /// <response code="200">The game was found and successfully updated</response>
        /// <response code="404">The game was not found</response>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CalculateNextGeneration(string id)
        {
            string fileName = $"game_{id}.json";
            game.nextGen(id);
            return Ok("Game updated with new generation with 2.0 API version");
        }



    }
}
