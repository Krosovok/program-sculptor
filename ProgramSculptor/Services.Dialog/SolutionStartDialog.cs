using System.Windows;
using DialogViewModel;
using Model;

namespace Services.Dialog
{
    public class SolutionStartDialog : ISolutionStartDialog
    {
        public SolutionStartDialog(Task task, IMessageService messageService, ISourceShowerService sourceShower)
        {
            Task = task;
            MessageService = messageService;
            SourceShower = sourceShower;
        }
        
        public Task Task { get; }
        public IMessageService MessageService { get; }
        public ISourceShowerService SourceShower { get; }

        public Solution ShowDialog()
        {
            SolutionChoice viewModel = new SolutionChoice(Task, MessageService, SourceShower);
            Window dialog = new SolutionStartDialogWindow() {DataContext = viewModel};

            bool? success = dialog.ShowDialog();

            if (success != true)
            {
                return null;
            }
            else
            {
                return viewModel.Solution;
            }
        }
    }
}
