using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramSculptor.Model;
using System.Linq;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;

namespace Test.ProgramSculptor.Model
{
    [TestClass()]
    public class OrdinalModelTest
    {
        private const int Turns = 50;
        private const int Count = 20;

        [TestMethod()]
        public void TestOrdinalModelTimeTicking()
        {
            OrdinalModel model = OrdinalModel();

            for (int i = 0; i < Turns; i++)
            {
                model.NextTurn();
            }

            Assert.IsTrue(model.Field.AllCells
                .Select(cell => cell.Standing)
                .OfType<TickerElement>()
                .All(ticker => ticker.Turns == Turns));
        }

        [TestMethod()]
        public void TestInitialize()
        {
            OrdinalModel model = OrdinalModel();

            Assert.IsTrue(model.Field.AllCells
                              .Count(cell => cell.Standing is TickerElement) == Count);
        }

        private static OrdinalModel OrdinalModel()
        {
            FieldParameters parameters = new FieldParameters() {Size = 10};
            OrdinalModel model = new OrdinalModel(parameters);

            InitializationData data = new InitializationData(typeof(TickerElement))
            {
                Count = Count,
                InitialazerType = typeof(RandomInitializer)
            };
            Initializer initializer = Initializer.FromInitializationData(data);

            model.Initialize(new[] {initializer});
            return model;
        }
    }
}