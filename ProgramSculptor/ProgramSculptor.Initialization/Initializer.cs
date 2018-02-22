using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProgramSculptor.Core;
using System.Windows.Media;

namespace ProgramSculptor.Initialization
{
    public abstract class Initializer
    {
        public static readonly Type[] InitializatorTypes =
        {
            typeof(RandomInitializer)
        };
        
        public Type ElementType { get; }
        public int Count { get; set; }
        public Color Color { get; set; } 
        
        protected Initializer(Type elementType)
        {
            if (!typeof(Element).IsAssignableFrom(ElementType))
            {
                throw new InitializationException("Given type does not inherits class Element or NonPassableElement.");
            }
            ElementType = elementType;
        }

        public static Initializer FromInitializationData(InitializationData data)
        {
            Initializer result = GetInitializerOfType(data.InitialazerType, data.Type);
            result.Count = data.Count;
            result.Color = data.Color;

            return result;
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

        private static Initializer GetInitializerOfType(Type initialazerType, Type userType)
        {
            ConstructorInfo constructor = initialazerType.GetConstructor(
                new[] { typeof(Type) });
            return (Initializer) constructor.Invoke(
                new object[]{ userType});
        }

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