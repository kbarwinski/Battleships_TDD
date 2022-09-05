using Battleships_TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD_Tests
{
    public static class Board_TestsData
    {
        public static IEnumerable<object[]> PlaceShips_NotEnoughSpace_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Board(1,1),
                    new List<Ship>()
                    {
                        new Ship("test1",1,1),
                        new Ship("test2",1,1),
                    }
                },
                new object[]
                {
                    new Board(2,2),
                    new List<Ship>()
                    {
                        new Ship("test1",2,1),
                        new Ship("test2",2,1),
                        new Ship("test3",2,1),
                    }
                },
            };
        }

        public static IEnumerable<object[]> PlaceShips_MultipleShips_Data()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new Board(2,2),
                    new List<Ship>()
                    {
                        new Ship("test1",2,1),
                        new Ship("test2",2,1),
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
