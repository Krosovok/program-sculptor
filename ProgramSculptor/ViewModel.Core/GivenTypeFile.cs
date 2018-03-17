using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Types;

namespace ViewModel.Core
{
    public class GivenTypeFile : ClassFileViewModel
    {
        public GivenTypeFile(Task parent, ClassFile source, IMessageService messageService) 
            : base(parent, source, messageService)
        {
        }

        protected override string GetContent(Task task, ClassFile classFile)
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            return dao.GivenTypesFileContents(task, classFile);
        }
    }
}