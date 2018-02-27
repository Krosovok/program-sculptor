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
        private const string DllName = "ProgramSculptor.Core.dll";
        private const string GivenTypesDll = "GivenTypes.dll";
        private const string DLL = "System.Core.dll";
        private const string ErrorFormat = "Error ({0}): {1}";

        private readonly CSharpCodeProvider provider = new CSharpCodeProvider();
        private readonly CompilerParameters parameters = new CompilerParameters();
        private readonly List<string> givenTypes = new List<string>();

        public TypeCompiler()
        {
            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add(DllName);
            // TODO: Make some list of needed Dlls in separagte file or config.
            parameters.ReferencedAssemblies.Add(DLL);
            //
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
                CompileAssembly(code, parameters));
        }

        private void AddGivenTypes()
        {
            CompilerParameters compilerParameters = GivenTypesParameters();

            Assembly givenTypesAssembly = CompileAssembly(givenTypes, compilerParameters);
            parameters.ReferencedAssemblies.Add(GivenTypesDll);
        }

        private CompilerParameters GivenTypesParameters()
        {
            string[] references = new string[parameters.ReferencedAssemblies.Count];
            parameters.ReferencedAssemblies.CopyTo(references, 0);

            CompilerParameters compilerParameters = new CompilerParameters(
                references, GivenTypesDll)
            {
                GenerateInMemory = false,
                GenerateExecutable = false
            };
            return compilerParameters;
        }

        private Assembly CompileAssembly(IEnumerable<string> code, CompilerParameters compilerParameters)
        {
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters, code.ToArray());

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