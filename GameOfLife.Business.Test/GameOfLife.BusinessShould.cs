using FluentAssertions;
using GameOfLife.API.Test;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Business.Test
{
    public class GameOfLifeShould
    {
        private BoardRepository boardRepository;
        private GameOfLife game;
        private string gameId;

        [SetUp]
        public void SetUp()
        {
            boardRepository = Substitute.For<BoardRepository>();
            game = new GameOfLife(boardRepository);
            gameId = "abc123.json";

        }
        [Test]

        public void should_change_state_to_death_when_surronded_three()
        {
            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(0, 1)
            .SetAliveCell(1, 0)
            .SetAliveCell(1, 1)
            .Build();

            bool[][] expectedBoard = new BoardBuilder(4, 4)
            .SetAliveCell(0, 1)
            .SetAliveCell(0, 0)
            .SetAliveCell(1, 0)
            .SetAliveCell(1, 1)
            .Build();

            game.NewGame(initialBoard,gameId);
            boardRepository.Load(gameId).Returns(new Board(initialBoard));
            game.nextGen(gameId);

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard, gameId);

            game.Equals(expected).Should().BeTrue();



        }


        [Test]

        public void should_change_state_to_death_when_less_than_two_alives_sourround_it()
        {
            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(1, 0)
            .Build();


            bool[][] expectedBoard = new BoardBuilder(4, 4)
            .Build();

            game.NewGame(initialBoard, gameId);
            boardRepository.Load(gameId).Returns(new Board(initialBoard));
            game.nextGen(gameId);

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard, gameId);

            game.Equals(expected).Should().BeTrue();



        }

        [Test]

        public void should_change_state_to_death_when_more_than_three_alive_sourround_it()
        {

            bool[][] initialBoard = new BoardBuilder(5, 5)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .SetAliveCell(2, 3)
            .SetAliveCell(3, 2)
            .Build();


            bool[][] expectedBoard = new BoardBuilder(5, 5)
            .SetAliveCell(1, 1)
            .SetAliveCell(1, 2)
            .SetAliveCell(1, 3)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 3)
            .SetAliveCell(3, 1)
            .SetAliveCell(3, 2)
            .SetAliveCell(3, 3)
            .Build();



            game.NewGame(initialBoard, gameId);
            boardRepository.Load(gameId).Returns(new Board(initialBoard));
            game.nextGen(gameId);

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard, gameId);

            game.Equals(expected).Should().BeTrue();
        }


        [Test]
        public void should_save_the_new_game()
        {


            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(1, 1)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .Build();

            game.NewGame(initialBoard,gameId);


            boardRepository.Received(1).Save(Arg.Is<Board>(board => board.Equals(new Board(initialBoard))), Arg.Any<string>());

        }

        [Test]
        public void should_save_the_next_game()
        {
            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(1, 1)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .Build();


            game.NewGame(initialBoard,gameId);
            boardRepository.Load(gameId).Returns(new Board(initialBoard));
            game.nextGen(gameId);

            boardRepository.Received(2).Save(Arg.Is<Board>(board => board.Equals(new Board(initialBoard))), Arg.Any<string>());
        }

        [Test]
        public void should_load_the_next_game()
        {
            bool[][] initialBoard = new BoardBuilder(4, 4)
            .SetAliveCell(1, 1)
            .SetAliveCell(1, 2)
            .SetAliveCell(2, 1)
            .SetAliveCell(2, 2)
            .Build();

            game.NewGame(initialBoard, gameId);
            boardRepository.Load(gameId).Returns(new Board(initialBoard));
            game.nextGen(gameId);

            boardRepository.Received(1).Load(gameId);
        }
    }
    

}
