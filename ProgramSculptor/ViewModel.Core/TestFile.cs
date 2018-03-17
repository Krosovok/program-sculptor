using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Types;

namespace ViewModel.Core
{
    public class TestFile : ClassFileViewModel
    {
        public TestFile(Task parent, ClassFile source, IMessageService messageService) 
            : base(parent, source, messageService)
        {
        }

        protected override string GetContent(Task task, ClassFile classFile)
        {
            return Dao.Factory.ClassFileDao.TestFileContents(task, classFile);
        }
    }
}