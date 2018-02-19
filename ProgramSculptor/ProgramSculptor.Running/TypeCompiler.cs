using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using ProgramSculptor.Core;

namespace ProgramSculptor.Running
{
    public class TypeCompiler
    {
        private const string ErrorFormat = "Error ({0}): {1}";
        
        private readonly CSharpCodeProvider provider = new CSharpCodeProvider();
        private readonly CompilerParameters parameters = new CompilerParameters();
        private readonly List<string> givenTypes = new List<string>();

        public TypeCompiler()
        {
            parameters.GenerateInMemory = true;
            
            Assembly core = Assembly.GetAssembly(typeof(Element));
            parameters.ReferencedAssemblies.Add(core.FullName);
        }

        public Assembly Compile(params string[] code)
        {
            Assembly givenTypesAssembly = CompileAssembly(givenTypes);
            // TODO: Check if it works.
            parameters.ReferencedAssemblies.Add(givenTypesAssembly.FullName);
            
            return CompileAssembly(code);
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

        public void AddGivenTypes(params string[] givenTypesCode)
        {
            givenTypes.AddRange(givenTypesCode);
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