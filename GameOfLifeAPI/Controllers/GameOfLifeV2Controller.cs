using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using Asp.Versioning;
using GameOfLife.API.Test;

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
        /// <response code="201">Returns the newly created game id</response>
        /// <response code="400">The input values are not valid</response>
        /// 
        /// <remarks>
        /// 
        /// Sample request:
        ///
        /// POST /api/v2/gameoflife
        /// 
        /// Content-Type: application/json
        /// 
        /// Request Body:
        /// 
        /// [
        /// 
        ///     [3,3],          // Board dimensions
        ///     
        ///     [0,1],          // Living cell
        ///     
        ///     [1,0],          // Living cell
        ///     
        ///     [1,1]
        ///     
        /// ]
        ///     
        ///
        /// </remarks>



        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]

        public IActionResult CreateGame([FromBody] int[][] initialBoard)
        {
            generateId = Guid.NewGuid().ToString();

            if (initialBoard[0].Length != 2) { return BadRequest(); }


            BoardBuilder board = new BoardBuilder(initialBoard[0][0], initialBoard[0][1]);

            for(int i = 1; i<initialBoard.Length; i++)
            {
                board.SetAliveCell(initialBoard[i][0], initialBoard[i][1]);
            }



            bool[][] bools = board.Build();

            game.NewGame(bools, generateId);
            var response = new
            {
                Message = "Game created and saved with 2.0 API version",
                GameId = generateId
            };

            return Ok(response);
        }

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
