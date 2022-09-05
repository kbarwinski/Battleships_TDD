using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Battleships_TDD;

namespace Battleships_TDD_Tests
{
    public class Board_Tests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void Board_InvalidBoardSize_ThrowsArgumentOutOfRangeException(int height, int width)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(height, width));
        }

        [Theory]
        [InlineData(3, 4)]
        public void Board_ValidArguments_ReturnsBoard(int height, int width)
        {
            var board = new Board(height, width);

            Assert.Equal(height * width, board.Fields.Count);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var field = board.Fields.ElementAt(j + width * i);
                    Assert.Equal(i, field.Row);
                    Assert.Equal(j, field.Column);
                    Assert.Equal(FieldType.Empty, field.FieldType);
                }
            }

        }

        [Theory]
        [InlineData(5, 0, 0, 6, 5)]
        [InlineData(2, 0, 0, 2, 3)]
        public void PlaceShip_OneTooLargeShip_ThrowsArgumentOutOfRangeException(int boardSize, int targetRow, int targetColumn, int shipWidth, int shipHeight)
        {
            var board = new Board(boardSize, boardSize);
            var ship = new Ship("test", shipHeight, shipWidth);

            Assert.Throws<ArgumentOutOfRangeException>(() => board.PlaceShip(targetRow, targetColumn, ship));
        }

        [Theory]
        [InlineData(5, 2, 0, 1, 4)]
        [InlineData(3, 0, 1, 3, 2)]
        public void PlaceShip_OneShipOutOfBounds_ReturnsFalse(int boardSize, int targetRow, int targetColumn, int shipWidth, int shipHeight)
        {
            var board = new Board(boardSize, boardSize);
            var ship = new Ship("test", shipHeight, shipWidth);

            Assert.False(board.PlaceShip(targetRow, targetColumn, ship));
            Assert.Contains(board.GetFieldRange(targetRow, targetRow + shipHeight, targetColumn, targetColumn + shipWidth)
                , f => f.FieldType == FieldType.Empty && f.OwnedBy == null);
        }

        [Theory]
        [InlineData(5, 0, 0, 2, 3)]
        [InlineData(2, 0, 0, 2, 2)]
        public void PlaceShip_OnePlaceableShip_ReturnsTrue(int boardSize, int targetRow, int targetColumn, int shipWidth, int shipHeight)
        {
            var board = new Board(boardSize, boardSize);
            var ship = new Ship("test", shipHeight, shipWidth);

            Assert.True(board.PlaceShip(targetRow, targetColumn, ship));
            Assert.Contains(board.GetFieldRange(targetRow, targetRow + shipHeight, targetColumn, targetColumn + shipWidth)
                , f => f.FieldType == FieldType.Ship && f.OwnedBy == ship);
        }

        [Fact]
        public void PlaceShip_TwoOverlappingShips_ReturnsFalse()
        {
            var board = new Board(6, 6);
            var ship1 = new Ship("test1", 3, 1);
            var ship2 = new Ship("test2", 2, 1);

            Assert.True(board.PlaceShip(0, 0, ship1));
            Assert.False(board.PlaceShip(2, 0, ship2));
        }

        [Theory]
        [InlineData(5, 6, 5)]
        [InlineData(2, 2, 3)]
        public void PlaceShips_OneTooLargeShip_ThrowsArgumentOutOfRangeException(int boardSize, int shipWidth, int shipHeight)
        {
            var board = new Board(boardSize, boardSize);
            var ship = new Ship("test", shipHeight, shipWidth);

            Assert.Throws<ArgumentOutOfRangeException>(() => board.PlaceShips(new List<Ship>() { ship }));
        }

        [Theory]
        [InlineData(5,2,2)]
        [InlineData(4,4,4)]
        public void PlaceShips_OneShip_PlacesShipRandomly(int boardSize, int shipWidth, int shipHeight)
        {
            var board = new Board(boardSize, boardSize);
            var ship = new Ship("test", shipHeight, shipWidth);

            board.PlaceShips(new List<Ship> { ship });
            Assert.Equal(shipWidth * shipHeight, board.Fields.Count(f => f.OwnedBy == ship));
        }

        [Theory]
        [MemberData(nameof(Board_TestsData.PlaceShips_NotEnoughSpace_Data), MemberType = typeof(Board_TestsData))]
        public void PlaceShips_NotEnoughSpace_ThrowsInvalidOperationException(Board board, List<Ship> ships)
        {
            Assert.Throws<InvalidOperationException>(() => board.PlaceShips(ships));
        }

        [Theory]
        [MemberData(nameof(Board_TestsData.PlaceShips_MultipleShips_Data), MemberType = typeof(Board_TestsData))]
        public void PlaceShips_MultipleShips_PlacesShipsRandomly(Board board, List<Ship> ships)
        {
            board.PlaceShips(ships);

            foreach(var ship in ships)
            {
                Assert.Equal(ship.Width * ship.Height, board.Fields.Count(f => f.OwnedBy == ship));
            }
        }
    }
}
