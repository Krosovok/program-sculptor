using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProgramSculptor.Core;

namespace ProgramSculptor.Initialization
{
    public class RandomInitializer : Initializer
    {
        private readonly Random random = new Random();

        public RandomInitializer(Type elementType) : base(elementType)
        {
        }

        protected override Cell FindNextCell(Field field)
        {
            Cell place = null;
            while (place == null)
            {
                place = RandomCell(field);
            }
            return place;
        }

        private Cell RandomCell(Field field)
        {
            int x = random.Next(field.Size);
            int y = random.Next(field.Size);

            return IsValidCellToPopulate(field, x, y) ? 
                field[x, y] : 
                null;
        }

        private bool IsValidCellToPopulate(Field field, int x, int y)
        {
            bool isNonPassable = typeof(NonPassableElement).IsAssignableFrom(ElementType);
            bool cellIsFree = field[x, y].IsFree;
            return !isNonPassable || cellIsFree;
        }
    }
}