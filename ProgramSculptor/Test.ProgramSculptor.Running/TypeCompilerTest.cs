using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramSculptor.Running;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.ProgramSculptor.Running
{
    [TestClass()]
    public class TypeCompilerTest
    {
        private const string Extension = ".cs";
        private const string TestClass = "TestClass";
        private const string UsingTestClass = "UsingTestClass";

        private readonly string testClassContent = 
            File.ReadAllText(TestClass + Extension);
        private readonly string usingTestClassContent = 
            File.ReadAllText(UsingTestClass + Extension);

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompileNothing()
        {
            TypeCompiler compiler = new TypeCompiler();

            compiler.Compile(new string[0]);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompileNull()
        {
            TypeCompiler compiler = new TypeCompiler();

            compiler.Compile(null);
        }

        [TestMethod()]
        public void TestCompileOneType()
        {
            TypeCompiler compiler = new TypeCompiler();

            CompiledModel oneType = compiler.Compile(new []{ testClassContent });
            Type type = oneType.GetType(TestClass);
            
            Assert.AreEqual(type.Name, TestClass);
        }

        [TestMethod()]
        public void TestCompileTwoTypes()
        {
            TypeCompiler compiler = new TypeCompiler();

            CompiledModel twoTypes = compiler.Compile(
                new []
                {
                    testClassContent,
                    usingTestClassContent
                });
            Type type = twoTypes.GetType(UsingTestClass);
            
            Assert.AreEqual(type.Name, UsingTestClass);
        }
        
        [TestMethod()]
        public void TestCompileWithGivenTypes()
        {
            TypeCompiler compiler = new TypeCompiler();
            compiler.AddGivenTypes(
                new []{ testClassContent});
            
            CompiledModel oneType = compiler.Compile(
                new[] { usingTestClassContent });
            
            Type type = oneType.GetType(UsingTestClass);

            Assert.AreEqual(type.Name, UsingTestClass);
        }
    }
}