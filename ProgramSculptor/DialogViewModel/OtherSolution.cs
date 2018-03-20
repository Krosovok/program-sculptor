using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services;
using ViewModel.Command;
using ViewModel.Types;

namespace DialogViewModel
{
    public class OtherSolution : ITypesContainer
    {
        public OtherSolution(Solution solution, IMessageService messageService, ISourceShowerService sourceShower)
        {
            Solution = solution;
            MessageService = messageService;
            SourceShower = sourceShower;
            ShowSourcesCommand = new RelayCommand<ClassFileViewModel>(
                file => SourceShower.ShowSource(file.FileName, file.Content));
        }
        
        public Solution Solution { get; }
        public IMessageService MessageService { get; }

        public IEnumerable<ClassFileViewModel> Types => GetSolutionTypes();
        public ISourceShowerService SourceShower { get; set; }
        public ICommand ShowSourcesCommand { get; }

        private IEnumerable<ClassFileViewModel> GetSolutionTypes()
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;

            IEnumerable<ClassFile> solutionFiles = TryGetFromDao(dao);

            return solutionFiles.Select(file => new OtherSolutionFile(Solution, file, MessageService));
        }

        private IEnumerable<ClassFile> TryGetFromDao(IClassFileDao dao)
        {
            try
            {
                return dao.GetOtherSolutionFiles(Solution);
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
                return new ClassFile[0];
            }
        }
    }
}