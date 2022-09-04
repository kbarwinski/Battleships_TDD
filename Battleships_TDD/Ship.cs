using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD
{
    public class Ship
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Ship(string name, int height, int width)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Provided name is null or empty.");
            if(height<=0 || width<=0)
                throw new ArgumentOutOfRangeException("At least one of the provided parameters is out of range.");

            Name = name;
            Height = height;
            Width = width;
        }



    }
}
