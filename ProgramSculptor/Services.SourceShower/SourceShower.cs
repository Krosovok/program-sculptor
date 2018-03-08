using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceShowerViewModel;

namespace Services.SourceShower
{
    public class SourceShower : ISourceShowerService
    {
        public void ShowSource(string fileName, string content)
        {
            SourceViewModel viewModel = new SourceViewModel(fileName, content);

            SourcePresenter presenter = new SourcePresenter() {DataContext = viewModel};
            
            presenter.Show();
        }
    }
}
