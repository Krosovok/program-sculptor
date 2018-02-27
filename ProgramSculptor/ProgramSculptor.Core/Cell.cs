using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Core
{
    public class Cell
    {
        private NonPassableElement standing;

        public Cell(Field field, int x, int y)
        {
            Field = field;
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public NonPassableElement Standing
        {
            get { return standing; }
            internal set
            {
                standing = value;
                OnStandingElementChanged(standing);
            }
        }
        public bool IsFree => Standing == null;
        public IList<Element> Elements { get; } = new List<Element>();
        public IEnumerable<Cell> NearbyCells => Field.GetNearbyCells(this);
        private Field Field { get; }

        protected virtual void OnStandingElementChanged(Element standing)
        {
            StandingElementChanged?.Invoke(standing);
        }

        internal void AddElement(Element element)
        {
            Elements.Add(element);
        }

        internal void RemoveElement(Element element)
        {
            Elements.Remove(element);
        }

        public event Action<Element> StandingElementChanged;
    }
}