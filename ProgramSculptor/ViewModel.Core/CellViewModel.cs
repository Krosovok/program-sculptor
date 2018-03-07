using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using ProgramSculptor.Core;

namespace ViewModel.Core
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private readonly OrdinalFieldViewModel parent;
        private Color background;

        public CellViewModel(OrdinalFieldViewModel parent, Cell source)
        {
            this.parent = parent;
            source.StandingElementChanged += SourceOnStandingElementChanged;
        }

        public Color Background
        {
            get { return background; }
            set
            {
                background = value; 
                OnPropertyChanged(nameof(Background));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SourceOnStandingElementChanged(Element element)
        {
            Background = parent.TypeColor(element?.GetType());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}