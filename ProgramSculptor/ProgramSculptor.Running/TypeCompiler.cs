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
        // TODO: Make some list of needed Dlls in separate file or config.
        private readonly string[] referencedAssabmlies = {"System.Core.dll", "ProgramSculptor.Core.dll"};

        private const string GivenTypesDll = "GivenTypes.dll";
        private const string ErrorFormat = "Error ({0}): {1}";

        private readonly CSharpCodeProvider provider = new CSharpCodeProvider();
        private readonly CompilerParameters parameters;
        private readonly List<string> givenTypes = new List<string>();
        
        public TypeCompiler(string assemblyName)
        {
            parameters = assemblyName == null
                ? new CompilerParameters {GenerateInMemory = true}
                : new CompilerParameters(new string[0], assemblyName) {GenerateInMemory = false};
            parameters.ReferencedAssemblies.AddRange(referencedAssabmlies);
        }

        public CompiledModel Compile(IEnumerable<string> code)
        {
            if (givenTypes.Count != 0)
            {
                AddAdditionalTypes();
            }

            if (code == null || !code.Any())
            {
                throw new ArgumentException("Given code can't be null or empty.", nameof(code));
            }

            return new CompiledModel(
                CompileAssembly(code, parameters));
        }

        public void AddGivenTypes(IEnumerable<string> givenTypesCode)
        {
            givenTypes.AddRange(givenTypesCode);
        }

        public void AddReference(string dllName)
        {
            parameters.ReferencedAssemblies.Add(dllName);
        }

        private void AddAdditionalTypes()
        {
            CompilerParameters compilerParameters = GivenTypesParameters();

            CompileAssembly(givenTypes, compilerParameters);
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

        private CompilerResults CompileAssembly(IEnumerable<string> code, CompilerParameters compilerParameters)
        {
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters, code.ToArray());

            if (results.Errors.HasErrors)
            {
                results.TempFiles.Delete();
                throw GetExceptionFromErrors(results);
            }
            
            return results;
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