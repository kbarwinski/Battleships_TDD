using Battleships_TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD_Tests
{
    public class Game_TestsData
    {

        public static IEnumerable<object[]> Game_ValidArguments_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Board(5,5),
                    new List<Ship>()
                    {
                        new Ship("test1",1,3),
                        new Ship("test2",3,1),
                    }
                },
                new object[]
                {
                    new Board(10,10),
                    new List<Ship>()
                    {
                        new Ship("Battleship",1,5),
                        new Ship("Destroyer 1",1,4),
                        new Ship("Destroyer 2",1,4),
                    }
                }
            };
        }
    }
}
