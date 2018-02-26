using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Time;

namespace ProgramSculptor.Model
{
    public class OrdinalModel
    {
        private readonly TurnTicker turns = new TurnTicker();

        public OrdinalModel(FieldParameters fieldParameters)
        {
            Field = new Field(fieldParameters);
            turns.TurnStart += Field.NextTurn;
        }

        public Field Field { get; }

        public void Initialize(IEnumerable<Initializer> initializers)
        {
            foreach (Initializer initializer in initializers)
            {
                initializer.Initilize(Field);
            }
        }

        public void NextTurn() => turns.NextTurn();

        
    }
}
