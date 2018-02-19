using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Core
{
    public class NonPassableElement : Element
    {
        public NonPassableElement(Cell place) : base(place) { }
        
        protected sealed override void MoveTo(Cell other)
        {
            if (!other.IsFree)
            {
                throw new CollisionExeption("The target cell is not free.");
            }

            PerformMove(other);
        }

        private void PerformMove(Cell other)
        {
            if (Cell != null)
            {
                Cell.Standing = null;
            }
            other.Standing = this;
            base.MoveTo(other);
        }

    }
}
