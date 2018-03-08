using DataAccessInterfaces;
using Model;

namespace ViewModel.Core
{
    public class TestFile : ClassFileViewModel
    {
        public TestFile(Task parent, ClassFile source) : base(parent, source)
        {
        }

        protected override string GetContent(Task task, ClassFile classFile)
        {
            return Dao.Factory.ClassFileDao.TestFileContents(task, classFile);
        }
    }
}