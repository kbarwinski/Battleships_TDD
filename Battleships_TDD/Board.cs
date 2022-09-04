using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD
{
    public class Board
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Field> Fields { get; set; }

        public Board(int height, int width)
        {
            if(height<=0 || width <= 0)
                throw new ArgumentOutOfRangeException("At least one of the provided parameters is out of range.");
           

            Height = height;
            Width = width;

            Fields = new List<Field>();
            for(int i=0; i<height; i++)
            {
                for (int j = 0; j < width; j++)
                    Fields.Add(new Field(i, j));
            }
        }



    }
}
