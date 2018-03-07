using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramSculptor.Core;

namespace Test.ProgramSculptor.Model
{
    public class TickerElement : NonPassableElement
    {
        
        public TickerElement(Cell place) : base(place)
        {
            
        }

        public int Turns { get; private set; }

        public override void ActionOnTurn()
        {
            Turns++;
        }
    }
}
