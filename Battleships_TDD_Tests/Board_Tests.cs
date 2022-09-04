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
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(0,1)]
        public void Board_InvalidBoardSize_ThrowsArgumentOutOfRangeException(int height, int width)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Board(height, width));
        }

        [Theory]
        [InlineData(3,4)]
        public void Board_ValidArguments_ReturnsBoard(int height, int width)
        {
            var board = new Board(height, width);

            Assert.Equal(height * width, board.Fields.Count);
            for (int i=0; i<height; i++)
            {
                for(int j=0; j<width; j++)
                {
                    var field = board.Fields.ElementAt(j + width * i);
                    Assert.Equal(i, field.Row);
                    Assert.Equal(j, field.Column);
                    Assert.Equal(FieldType.Empty, field.FieldType);
                }
            }
 
        }
    }
}
