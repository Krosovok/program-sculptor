using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Services.Message
{
    public class MessageBoxService : IMessageService
    {
        public void Show(string message)
        {
            MessageBox.Show(message, "Message");
        }
    }
}
