using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramSculptor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ProgramSculptor.Core
{
    [TestClass]
    public class CellTest
    {

        [TestMethod]
        public void TestAddNonPassableElement()
        {
            Cell cell = new Cell(null, 0, 0);

            NonPassableElement standing = new NonPassableElement(cell);
            
            Assert.AreEqual(standing, cell.Standing);
        }

        [TestMethod]
        public void TestIsFree()
        {
            Cell cell = new Cell(null, 0, 0);

            Assert.IsTrue(cell.IsFree);
        }
        
        [TestMethod]
        [ExpectedException(typeof(CollisionExeption))]
        public void TestReplaceStandingElement()
        {
            Cell cell = new Cell(null, 0, 0);
            
            NonPassableElement element1 = new NonPassableElement(cell);
            NonPassableElement element2 = new NonPassableElement(cell);
        }

        [TestMethod]
        public void TestMultipleElements()
        {
            Cell cell = new Cell(null, 0, 0);

            Element[] elements =
            {
                new Element(cell), 
                new Element(cell), 
                new Element(cell) 
            };
            
            Assert.IsTrue(elements.SequenceEqual(cell.Elements));
        }


        [TestMethod]
        public void TestMultipleElementsWithNonPassable()
        {
            Cell cell = new Cell(null, 0, 0);

            Element[] elements =
            {
                new Element(cell),
                new Element(cell),
                new Element(cell),
                new NonPassableElement(cell)
            };

            Assert.IsTrue(elements.SequenceEqual(cell.Elements));
        }
    }
}