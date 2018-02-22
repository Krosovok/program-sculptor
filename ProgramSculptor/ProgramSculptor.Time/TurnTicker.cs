using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Time
{
    public class TurnTicker
    {
        public void NextTurn()
        {
            TurnStart?.Invoke();
            Turn++;
            TurnEnd?.Invoke();
        }

        public int Turn { get; set; }

        public event Action TurnStart;
        public event Action TurnEnd;
    }
}
