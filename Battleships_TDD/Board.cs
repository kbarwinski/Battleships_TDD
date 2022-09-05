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

        public List<Field> GetFieldRange(int startingRow, int endingRow, int startingColumn, int endingColumn)
        {
            return Fields.Where(f =>
            f.Row >= startingRow &&
            f.Row <= endingRow &&
            f.Column >= startingColumn &&
            f.Column <= endingColumn).ToList();
        }

        public bool PlaceShip(int row, int column, Ship ship)
        {
            if (ship.Width > Width || ship.Height > Height)
                throw new ArgumentOutOfRangeException("Ship is too big for this board.");

            var endingRow = row + ship.Height - 1;
            var endingColumn = column + ship.Width - 1;

            List<Field> fieldsToFill = GetFieldRange(row, endingRow, column, endingColumn);

            //Checks if ship would be placed out of bounds 
            if (fieldsToFill.Count != ship.Width * ship.Height)
                return false;
            //Checks if target range fields contain another ship
            if (fieldsToFill.Any(f => f.FieldType != FieldType.Empty))
                return false;

            fieldsToFill.ForEach(f =>
            {
                f.FieldType = FieldType.Ship;
                f.OwnedBy = ship;
            });

            return true;
        }

        public void PlaceShips(List<Ship> ships)
        {
            var rand = new Random();

            foreach(var ship in ships)
            {
                var availableFields = Fields.Where(f => f.FieldType == FieldType.Empty).ToList();

                if (rand.Next(2) == 0)
                    ship.Flip();

                while(availableFields.Count > 0)
                {
                    var randomField = availableFields.ElementAt(rand.Next(availableFields.Count() - 1));

                    var placingRes = PlaceShip(randomField.Row, randomField.Column, ship);
                    if (!placingRes)
                    {
                        ship.Flip();
                        placingRes = PlaceShip(randomField.Row, randomField.Column, ship);

                        if (!placingRes)
                        {
                            availableFields.Remove(randomField);
                            continue;
                        }
                    }
                    break;
                }
                if (availableFields.Count == 0)
                    throw new InvalidOperationException("Not enough space to place ships.");
            }
        }
    }
}
