using ProgramSculptor.Core;

namespace Test.ProgramSculptor.Running
{
    public class UsingTestClass : Element
    {
        public UsingTestClass(Cell place) : base(place)
        {
            TestClassProperty = new TestClass {Property = 100};

        }

        public TestClass TestClassProperty { get; set; }
    }
}
