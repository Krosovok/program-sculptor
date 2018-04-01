using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;

namespace Test.ProgramSculptor.Initialization
{
    [TestClass]
    public class RandomInitializerTest
    {
        private const int InitializedCells = 50;
        private const int FieldSize = 100;
        private const int MoreThanCells = FieldSize * FieldSize + 1;

        [TestMethod]
        public void TestInitializedNonPassableCount()
        {
            Field field = FieldWithNonPassables(InitializedCells);

            Assert.IsTrue(IsEnoughElements(field, InitializedCells));
        }

        [TestMethod]
        public void TestInitilizedElementsCount()
        {
            Field field = FieldWithElements(InitializedCells);

            Assert.IsTrue(IsEnoughElements(field, InitializedCells));
        }

        [TestMethod]
        [ExpectedException(typeof(WorkflowException))]
        public void TestInitializedNonPassableTooMany()
        {
            Field field = FieldWithNonPassables(MoreThanCells);
        }

        [TestMethod]
        public void TestInitializedElementsTooMany()
        {
            Field field = FieldWithElements(MoreThanCells);

            Assert.IsTrue(IsEnoughElements(field, MoreThanCells));

        }

        private static Field FieldWithNonPassables(int initializedCells)
        {
            RandomInitializer initializer = new RandomInitializer(typeof(NonPassableElement))
            {
                Count = initializedCells
            };
            return InitializeField(initializer);
        }

        private static Field FieldWithElements(int initializedCells)
        {
            RandomInitializer initializer = new RandomInitializer(typeof(Element))
            {
                Count = initializedCells
            };

            Field field = InitializeField(initializer);
            return field;
        }

        private static Field InitializeField(Initializer initializer)
        {
            Field field = new Field(new FieldParameters() {Size = FieldSize});

            initializer.Initilize(field);
            return field;
        }

        private static bool IsEnoughElements(Field field, int neededCount)
        {
            return field.AllCells
                .Aggregate(0,
                    (count, cell) => 
                        count + cell.Elements.Count()) == neededCount;
        }
    }
}