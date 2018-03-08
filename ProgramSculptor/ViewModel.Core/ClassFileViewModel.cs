using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace ViewModel.Core
{
    public abstract class ClassFileViewModel
    {
        private readonly Task parent;
        private readonly ClassFile source;

        protected ClassFileViewModel(Task parent, ClassFile source)
        {
            this.parent = parent;
            this.source = source;
        }

        public string FileName => source.FileName;
        public string Content => GetContent(parent, source);
        
        protected abstract string GetContent(Task task, ClassFile classFile);
    }
}
