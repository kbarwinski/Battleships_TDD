using Battleships_TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleships_TDD_Tests
{
    public class Game_Tests
    {
        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Game_ValidArguments_ReturnsGame(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            Assert.False(game.HasFinished);
            Assert.Equal(ships, game.Ships);
            Assert.Equal(board, game.Board);
        }

        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Shoot_CoordinatesOutOfRange_ThrowsInvalidOperationException(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            Assert.Throws<InvalidOperationException>(() => game.Shoot(board.Width + 1, board.Height + 1 ));
        }

        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Shoot_AtEmptyField_SetsTargetFieldTypeToMiss(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            var emptyField = board.Fields.First(f => f.FieldType == FieldType.Empty);

            game.Shoot(emptyField.Row, emptyField.Column);

            Assert.Equal(FieldType.Miss, emptyField.FieldType);
        }

        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Shoot_AtShipField_SetsTargetFieldTypeToHit(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            var shipField = board.Fields.First(f => f.FieldType == FieldType.Ship);

            game.Shoot(shipField.Row, shipField.Column);

            Assert.Equal(FieldType.Hit, shipField.FieldType);
        }

        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Shoot_MultipleShotsAtShip_ShipIsSunk(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            var targetShip = ships.ElementAt(0);

            var shipFields = board.Fields.Where(f => f.OwnedBy == targetShip).ToList();
            foreach (var field in shipFields)
                game.Shoot(field.Row, field.Column);

            Assert.Equal(targetShip.Width * targetShip.Height, board.Fields.Count(f => f.FieldType == FieldType.Sunk));
            Assert.Contains(shipFields, f => f.OwnedBy == null && f.FieldType == FieldType.Sunk);
            Assert.DoesNotContain(ships, s => s == targetShip);
        }

        [Theory]
        [MemberData(nameof(Game_TestsData.Game_ValidArguments_Data), MemberType = typeof(Game_TestsData))]
        public void Shoot_MultipleShotsAtShips_GameHasFinished(Board board, List<Ship> ships)
        {
            var game = new Game(board, ships);
            var shipFields = board.Fields.Where(f => f.FieldType == FieldType.Ship).ToList();

            foreach (var field in shipFields)
                game.Shoot(field.Row, field.Column);

            Assert.Equal(shipFields.Count, board.Fields.Count(f => f.FieldType == FieldType.Sunk));
            Assert.True(game.HasFinished);
        }


    }
}
