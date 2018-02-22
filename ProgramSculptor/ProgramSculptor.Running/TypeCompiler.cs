using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace ProgramSculptor.Running
{
    public class TypeCompiler
    {
        private const string ErrorFormat = "Error ({0}): {1}";
        private const string DllName = "ProgramSculptor.Core.dll";

        private readonly CSharpCodeProvider provider = new CSharpCodeProvider();
        private readonly CompilerParameters parameters = new CompilerParameters();
        private readonly List<string> givenTypes = new List<string>();

        public TypeCompiler()
        {
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add(DllName);
        }

        public void AddGivenTypes(IEnumerable<string> givenTypesCode)
        {
            givenTypes.AddRange(givenTypesCode);
        }

        public CompiledModel Compile(IEnumerable<string> code)
        {
            if (givenTypes.Count != 0)
            {
                AddGivenTypes();
            }

            if (code == null || !code.Any())
            {
                throw new ArgumentException("Given code can't be null or empty.", nameof(code));
            }
            
            return new CompiledModel(
                CompileAssembly(code));
        }

        private void AddGivenTypes()
        {
            Assembly givenTypesAssembly = CompileAssembly(givenTypes);
            parameters.ReferencedAssemblies.Add(givenTypesAssembly.FullName);
        }

        private Assembly CompileAssembly(IEnumerable<string> code)
        {
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code.ToArray());

            if (results.Errors.HasErrors)
            {
                throw GetExceptionFromErrors(results);
            }

            return results.CompiledAssembly;
        }

        private static CodeException GetExceptionFromErrors(CompilerResults results)
        {
            StringBuilder sb = new StringBuilder();

            foreach (CompilerError error in results.Errors)
            {
                sb.AppendLine(string.Format(ErrorFormat, error.ErrorNumber, error.ErrorText));
            }

            return new CodeException(sb.ToString());
        }
    }
}