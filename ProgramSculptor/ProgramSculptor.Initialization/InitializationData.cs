using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProgramSculptor.Initialization
{
    public class InitializationData
    {
        private Type initialazerType;

        public InitializationData(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
        public int Count { get; set; } = 10;
        public Color Color { get; set; } = RandomColor();
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

    }
}
