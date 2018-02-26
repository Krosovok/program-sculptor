using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProgramSculptor.Initialization
{
    public class InitializationData : INotifyPropertyChanged
    {
        private Type initialazerType = typeof(RandomInitializer);
        private Color color = RandomColor();
        private int count = 10;

        public InitializationData(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public int Count
        {
            get { return count; }
            set
            {
                count = value; 
                OnPropertyChanged(nameof(Count));
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                color = value; 
                OnPropertyChanged(nameof(Color));
            }
        }

        public Type InitialazerType
        {
            get { return initialazerType; }
            set
            {
                if (!Initializer.InitializatorTypes.Contains(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                
                initialazerType = value;
                OnPropertyChanged(nameof(InitialazerType));
            }
        }
        
        private static Color RandomColor()
        {
            Random random = new Random();
            return Color.FromRgb(
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
