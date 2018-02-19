namespace ProgramSculptor.Core
{
    public class Element
    {
        public Element(Cell place)
        {
            MoveTo(place);
        }
        
        public Cell Cell { private set; get; }

        public virtual void ActionOnTurn() { }

        protected virtual void MoveTo(Cell other)
        {
            Cell?.RemoveElement(this);
            Cell = other;
            Cell?.AddElement(this);
        }
    }
}