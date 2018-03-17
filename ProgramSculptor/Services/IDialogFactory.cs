using Model;

namespace Services { 
    public interface IDialogFactory
    {
        IDialog NewDialog();

        ISolutionStartDialog NewSolutionStartDialog(Task task);
        
        IMessageService MessageService { get; set; }
        
        ISourceShowerService SourceShower { get; set; }
    }
}