using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Running
{
    public class CompiledModel
    {
        public CompiledModel(Assembly compiled)
        {
            this.Assembly = compiled;
        }

        private Assembly Assembly { get; }

        public Type GetType(string typeName)
        {
            return Assembly.ExportedTypes.Single(type => type.Name == typeName);
        }
    }
}
