using Services;

namespace ViewModel.Core
{
    public class MainViewModel
    {
        private IDialogFactory dialogFactory;

        public MainViewModel()
        {
            
        }

        public AllTasks Tasks { get; } = new AllTasks();
        public UserSession UserSession { get; } = new UserSession();

        public IDialogFactory DialogFactory
        {
            get { return dialogFactory; }
            set
            {
                dialogFactory = value;
                UserSession.DialogFactory = value;
            }
        }
    }
}
