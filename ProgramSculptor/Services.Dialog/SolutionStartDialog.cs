using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DialogViewModel;
using Model;

namespace Services.Dialog
{
    public class SolutionStartDialog 
    {
        public SolutionStartDialog(Task task)
        {
            Task = task;
        }
        
        public Task Task { get; }

        public Solution ShowDialog()
        {
            SolutionChoice viewModel = new SolutionChoice(Task);
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
