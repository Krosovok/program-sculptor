using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProgramSculptor.Core
{
    public struct FieldParameters : INotifyPropertyChanged
    {
        private int size;

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}