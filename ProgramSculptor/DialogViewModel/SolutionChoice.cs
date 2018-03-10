using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessInterfaces;
using Model;

namespace DialogViewModel
{
    public class SolutionChoice
    {
        private readonly string username;

        public SolutionChoice(Task solved)
        {
            Solved = solved;
            username = Dao.Factory.UserDao.CurrentUser;
        }
        
        public Task Solved { get; }

        public IEnumerable<Solution> OthersSolutions => GetOthersSolutions();

        public string SolutionName { get; set; }
        public Solution BaseSoluton { get; set; }
        
        public Solution Solution => new Solution(SolutionName, username, Solved, BaseSoluton?.Id);

        private IEnumerable<Solution> GetOthersSolutions()
        {
            Task previous = Solved.Chain.PreviousOf(Solved);
            ISolutionDao dao = Dao.Factory.SolutionDao;
            return dao.GetOthersSolutions(previous, username);
        }
    }
}
