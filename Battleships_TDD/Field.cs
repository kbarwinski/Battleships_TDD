using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD
{
    public class Field
    {
        public Field(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("At least one of the provided parameters is out of range.");

            Row = row;
            Column = column;
            FieldType = FieldType.Empty;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public FieldType FieldType { get; set; }
    }
}
