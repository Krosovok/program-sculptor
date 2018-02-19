using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Core
{
    public class Field
    {
        private readonly Cell[,] field;

        public Field(FieldParameters parameters)
        {
            Size = parameters.Size;
            this.field = new Cell[Size, Size];
            FillWithCells();
        }

        public Cell this[int x, int y] => field[x, y];

        public int Size { get; }
        
        public IEnumerable<Cell> GetNearbyCells(Cell cell)
        {
            int minX = cell.X == 0 ? 0 : cell.X - 1;
            int minY = cell.Y == 0 ? 0 : cell.Y - 1;

            int maxX = cell.X == Size - 1 ? Size - 1 : cell.X + 1;
            int maxY = cell.Y == Size - 1 ? Size - 1 : cell.Y + 1;
            return GetOthersInRange(cell, minX, minY, maxX, maxY);
        }

        public IEnumerable<Cell> GetAllCells()
        {
            return field.Cast<Cell>();
        }

        public void NextTurn()
        {
            foreach (Cell cell in field)
            {
                foreach (Element cellElement in cell.Elements)
                {
                    cellElement.ActionOnTurn();
                }
            }
        }

        private void FillWithCells()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    field[x, y] = new Cell(x, y);
                }
            }
        }


        private IEnumerable<Cell> GetOthersInRange(Cell cell, int minX, int minY, int maxX, int maxY)
        {
            IList<Cell> cells = new List<Cell>();
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Cell found = this[x, y];
                    if (found != cell)
                    {
                        cells.Add(found);
                    }
                }
            }
            return cells;
        }
    }
}
