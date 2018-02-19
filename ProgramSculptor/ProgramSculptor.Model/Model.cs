using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Time;

namespace ProgramSculptor.Model
{
    public class Model
    {
        private readonly Field field;
        private readonly TurnTicker turns = new TurnTicker();

        public Model(FieldParameters fieldParameters)
        {
            field = new Field(fieldParameters);
            turns.TurnStart += field.NextTurn;
        }
    }
}
