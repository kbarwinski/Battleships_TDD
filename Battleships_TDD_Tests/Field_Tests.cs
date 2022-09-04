using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Battleships_TDD;
namespace Battleships_TDD_Tests
{
    public class Field_Tests
    {
        [Theory]
        [InlineData(-1,0)]
        [InlineData(0,-1)]
        [InlineData(-1,-1)]
        public void Field_InvalidRowAndOrColumn_ThrowsArgumentOutOfRangeException(int row, int column)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Field(row, column));
        }

        [Theory]
        [InlineData(1,2)]
        public void Field_ValidArguments_ReturnsEmptyField(int row, int column)
        {
            var field = new Field(row, column);

            Assert.Equal(1, field.Row);
            Assert.Equal(2, field.Column);
            Assert.Equal(FieldType.Empty, field.FieldType);
        }
    }
}
