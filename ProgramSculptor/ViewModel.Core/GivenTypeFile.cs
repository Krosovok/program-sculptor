using DataAccessInterfaces;
using Model;

namespace ViewModel.Core
{
    public class GivenTypeFile : ClassFileViewModel
    {
        public GivenTypeFile(Task parent, ClassFile source) : base(parent, source)
        {
        }

        protected override string GetContent(Task task, ClassFile classFile)
        {
            return Dao.Factory.ClassFileDao.GivenTypesFileContents(task, classFile);
        }
    }
}