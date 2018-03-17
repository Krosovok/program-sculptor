using System;
using DataAccessInterfaces;
using Model;
using Services;

namespace ViewModel.Types
{
    public abstract class ClassFileViewModel
    {
        private readonly Task parent;
        private readonly ClassFile source;
        private readonly IMessageService messageService;

        protected ClassFileViewModel(Task parent, ClassFile source, IMessageService messageService)
        {
            this.parent = parent;
            this.source = source;
            this.messageService = messageService;
        }

        public string FileName => source.FileName;
        public string Content => TryGetContent(parent, source);

        private string TryGetContent(Task task, ClassFile classFile)
        {
            try
            {
                return GetContent(task, classFile);
            }
            catch (DataAccessException e)
            {
                messageService.Show(e.Message);
                return string.Empty;
            }
        }

        protected abstract string GetContent(Task task, ClassFile classFile);
    }
}
