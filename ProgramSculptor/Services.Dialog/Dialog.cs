using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Services.Dialog
{
    public class Dialog : IDialog
    {
        private const string DefaultTitle = "Name";
        private const string DefaultMessage = "What will be it's name?";

        public string Title { get; set; } = DefaultTitle;
        public string Message { get; set; } = DefaultMessage;
        public string Value { get; set; } = string.Empty;

        public string ShowNameDialog()
        {
            NameDialog dialog = new NameDialog(this);
            return dialog.ShowDialog() == true ? 
                this.Value :
                null;
        }
        
        public string[] ShowOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = Title
            };

            return openFileDialog.ShowDialog() == true ? 
                openFileDialog.FileNames : 
                new string[0];
        }

        public bool ShowLoginDialog()
        {
            LoginDialog dialog = new LoginDialog();
            return dialog.ShowDialog() == true;
        }
    }
}
