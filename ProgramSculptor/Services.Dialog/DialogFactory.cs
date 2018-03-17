
using Model;

namespace Services.Dialog
{
    public class DialogFactory : IDialogFactory
    {
        public IDialog NewDialog() => new Dialog();
        public ISolutionStartDialog NewSolutionStartDialog(Task task) => new SolutionStartDialog(task, MessageService, SourceShower);
        public IMessageService MessageService { get; set; }
        public ISourceShowerService SourceShower { get; set; }
    }
}
