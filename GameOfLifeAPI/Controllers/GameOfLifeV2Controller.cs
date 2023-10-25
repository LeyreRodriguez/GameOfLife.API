using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using Asp.Versioning;

namespace GameOfLife.Business
{
    [ApiVersion("2.0")]
    [Route("api/v2/gameoflife")]
    [ApiController]
    public class GameOfLifeV2Controller : ControllerBase
    {
        private GameOfLife game;
        private string generateId;

        public GameOfLifeV2Controller(GameOfLife game)
        {
            this.game = game;
        }

        /// <summary>
        /// Return actual generation of GameOfLife and save in a JSON File.
        /// </summary>
        /// <param name="initialBoard"></param>
        /// <returns>A JSON File </returns>



        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public IActionResult CreateGame([FromBody] int[][] initialBoard)
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
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))] // Tipo personalizado para el ejemplo
        public IActionResult CalculateNextGeneration(string id)
        {
            string fileName = $"game_{id}.json";
            game.nextGen(id);
            return Ok("Game updated with new generation with 2.0 API version");
        }



     


    }
}
