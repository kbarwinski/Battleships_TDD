using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships_TDD
{
    public class Game
    {
        public Board Board { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasFinished { get { return Ships.Count == 0; } }

        public Game(Board board, List<Ship> ships)
        {
            Board = board;
            Ships = ships;

            board.PlaceShips(ships);
        }

        public void Shoot(int row, int column)
        {
            var targetField = Board.Fields.First(f=> f.Row == row && f.Column == column);

            switch (targetField.FieldType)
            {
                case FieldType.Empty:
                    targetField.FieldType = FieldType.Miss;
                    break;
                case FieldType.Ship:
                    targetField.FieldType = FieldType.Hit;

                    var shipFields = Board.Fields.Where(f => f.OwnedBy == targetField.OwnedBy).ToList();
                    if(shipFields.All(f => f.FieldType == FieldType.Hit))
                    {
                        Ships.Remove(targetField.OwnedBy);

                        foreach(var field in shipFields)
                        {
                            field.OwnedBy = null;
                            field.FieldType = FieldType.Sunk;
                        }
                    }
                    break;
            }
        }
    }
}
