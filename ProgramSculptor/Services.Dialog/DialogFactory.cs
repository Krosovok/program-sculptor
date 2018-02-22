using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dialog
{
    public class DialogFactory : IDialogFactory
    {
        
        public IDialog NewDialog() => new Dialog();
    }
}
