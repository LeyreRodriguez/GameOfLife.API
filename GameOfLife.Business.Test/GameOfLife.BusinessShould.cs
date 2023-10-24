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

        [SetUp]
        public void SetUp()
        {
            boardRepository = Substitute.For<BoardRepository>();
            game = new GameOfLife(boardRepository);

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

            game.NewGame(initialBoard);
            boardRepository.Load().Returns(new Board(initialBoard));
            game.nextGen();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard);

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

            game.NewGame(initialBoard);
            boardRepository.Load().Returns(new Board(initialBoard));
            game.nextGen();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard);

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



            game.NewGame(initialBoard);
            boardRepository.Load().Returns(new Board(initialBoard));
            game.nextGen();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(expectedBoard);

            game.Equals(expected).Should().BeTrue();
        }
    }
    

}
