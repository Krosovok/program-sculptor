using System;
using System.Drawing;
using System.Linq;
using ProgramSculptor.Core;

namespace ProgramSculptor.Initialization
{
    public abstract class Initializator
    {
        public Type ElementType { get; }
        public int Count { get; set; }
        public Color Color { get; set; }

        protected Initializator(Type elementType)
        {
            if (!typeof(Element).IsAssignableFrom(ElementType))
            {
                throw new InitializationException("Given type does not inherits class Element or NonPassableElement.");
            }
            ElementType = elementType;
        }
        
        public void Initilize(Field field)
        {
            CheckPlaceLeft(field);

            for (int i = 0; i < Count; i++)
            {
                Cell place = FindNextCell(field);
                ElementType
                    .GetConstructor(
                        new[] { typeof(Cell) })
                    .Invoke(
                        new[] { place });
            }
        }

        protected abstract Cell FindNextCell(Field field);

        private void CheckPlaceLeft(Field field)
        {
            bool isNotPassable = typeof(NonPassableElement).IsAssignableFrom(ElementType);
            if (isNotPassable && !PlaceLeft(field))
            {
                throw new InitializationException("Number of free cells is less than number of cells to be populated.");
            }
        }

        private bool PlaceLeft(Field field)
        {
            return Count <= field.GetAllCells().Count(cell => cell.IsFree);
        }
    }
}