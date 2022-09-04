using Battleships_TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleships_TDD_Tests
{
    public class Ship_Tests
    {
        [Theory]
        [InlineData(null,2,3)]
        [InlineData("",3,4)]
        public void Ship_NameIsNullOrEmpty_ThrowsArgumentException(string name, int height, int width)
        {
            Assert.Throws<ArgumentException>(() => new Ship(name, height, width));
        }

        [Theory]
        [InlineData("test",0,1)]
        [InlineData("test",1,0)]
        [InlineData("test",0,0)]
        public void Ship_InvalidShipSize_ThrowsArgumentOutOfRangeException(string name, int height, int width)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ship(name, height, width));
        }

        [Theory]
        [InlineData("test",1,5)]
        public void Ship_ValidArguments_ReturnsShip(string name, int height, int width)
        {
            var ship = new Ship(name, height, width);

            Assert.Equal(name, ship.Name);
            Assert.Equal(height, ship.Height);
            Assert.Equal(width, ship.Width);
        }

        [Theory]
        [InlineData("test",1,2)]
        [InlineData("test",2,3)]
        public void Flip_ValidShips_FlipsShipDimensions(string name, int height, int width)
        {
            var ship = new Ship(name, height, width);

            ship.Flip();

            Assert.Equal(width, ship.Height);
            Assert.Equal(height, ship.Width);
        }
    }
}
