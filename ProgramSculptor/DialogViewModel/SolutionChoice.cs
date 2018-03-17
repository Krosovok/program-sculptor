using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataAccessInterfaces;
using Model;
using Services;

namespace DialogViewModel
{
    public class SolutionChoice : INotifyPropertyChanged
    {
        private readonly string username;
        private string solutionName;
        private OtherSolution baseSoluton;

        public SolutionChoice(Task solved, IMessageService messageService, ISourceShowerService sourceShower)
        {
            Solved = solved;
            MessageService = messageService;
            username = Dao.Factory.UserDao.CurrentUser;
            SourceShower = sourceShower;
        }
        
        public Task Solved { get; }
        public IMessageService MessageService { get; }
        public ISourceShowerService SourceShower { get; }

        public IEnumerable<OtherSolution> OthersSolutions => GetOthersSolutions();

        public string SolutionName
        {
            get { return solutionName; }
            set
            {
                solutionName = value;
                OnPropertyChanged(nameof(AllSelected));
            }
        }

        public OtherSolution BaseSoluton
        {
            get { return baseSoluton; }
            set
            {
                baseSoluton = value; 
                OnPropertyChanged(nameof(AllSelected));
            }
        }

        public Solution Solution => new Solution(SolutionName, username, Solved, BaseSoluton.Solution.Id);
        public bool AllSelected => !string.IsNullOrEmpty(SolutionName) && BaseSoluton != null;

        private IEnumerable<OtherSolution> GetOthersSolutions()
        {
            Task previous = Solved.Chain.PreviousOf(Solved);
            ISolutionDao dao = Dao.Factory.SolutionDao;
            return TryGetSolutions(dao, previous)
                .Select(solution => new OtherSolution(solution, MessageService, SourceShower))
                .ToList();
        }

        private IEnumerable<Solution> TryGetSolutions(ISolutionDao dao, Task previous)
        {
            try
            {
                return dao.GetOthersSolutions(previous, username);
            }
            catch (DataAccessException e)
            {
                MessageService.Show(e.Message);
                return new Solution[0];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
