namespace Services
{
    public interface IDialog
    {
        string Message { get; set; }
        string Title { get; set; }
        string Value { get; set; }

        string ShowNameDialog();
        string[] ShowOpenFileDialog();
        bool ShowLoginDialog();
    }
}