using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramSculptor.Core;

namespace Test.ProgramSculptor.Core
{
    [TestClass]
    public class FieldTest
    {
        private const int Size = 50;
        private const int Last = Size - 1;
        private const int Center = 5;
        private const int NearbyCellsCount = 8;

        private readonly Field field = new Field(
            new FieldParameters
            {
                Size = Size
            });

        
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestFieldSizeOutOfRange()
        {
            Cell cell = field[Size, Size];
        }

        [TestMethod]
        public void TestFieldSizeBorder()
        {
            Cell cell = field[Last, 0];
            
            Assert.AreEqual(Last, cell.X);
        }

        [TestMethod]
        public void TestFieldSize()
        {
            Assert.AreEqual(Size, field.Size);
        }

        [TestMethod]
        public void TestGetNearbyCells()
        {
            IEnumerable<Cell> nearbyCells = field.GetNearbyCells(field[Center, Center]);
            
            Assert.IsTrue(
                nearbyCells.All(
                    cell => 
                        Math.Abs(cell.X - Center) == 1 ||
                        Math.Abs(cell.Y - Center) == 1));
        }

        [TestMethod]
        public void TestNearbyCellCount()
        {
            IEnumerable<Cell> nearbyCells = field.GetNearbyCells(field[Center, Center]);

            Assert.AreEqual(NearbyCellsCount, nearbyCells.Count());
        }
    }
}
