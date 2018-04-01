using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Running
{
    public class CompiledModel
    {
        public CompiledModel(CompilerResults compiled)
        {
            Compiled = compiled;
        }

        private CompilerResults Compiled { get; }
        private Assembly Assembly => Compiled.CompiledAssembly;

        public Type GetType(string typeName)
        {
            return Assembly.GetExportedTypes().First(type => type.Name.Contains(typeName));
        }

        public void Delete()
        {
            Compiled.TempFiles.Delete();
        }
    }
}