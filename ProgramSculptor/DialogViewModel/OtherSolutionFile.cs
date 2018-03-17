using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Types;

namespace DialogViewModel
{
    public class OtherSolutionFile : ClassFileViewModel
    {
        public OtherSolutionFile(Solution solution, ClassFile source, IMessageService messageService) 
            : base(solution.Task, source, messageService)
        {
            Solution = solution;
        }
        
        private Solution Solution { get; }
        
        protected override string GetContent(Task task, ClassFile classFile)
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            return dao.OtherSolutionFileContents(Solution, classFile);
        }
    }
}