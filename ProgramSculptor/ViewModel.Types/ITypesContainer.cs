using System.Collections.Generic;
using System.Windows.Input;
using Services;

namespace ViewModel.Types
{
    public interface ITypesContainer
    {
        IEnumerable<ClassFileViewModel> Types { get; }
        ISourceShowerService SourceShower { get; set; }
        ICommand ShowSourcesCommand { get; }
    }
}