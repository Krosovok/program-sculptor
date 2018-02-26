using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSculptor.Core
{
    public class Cell
    {
        private readonly IList<Element> elements = new List<Element>();
        private NonPassableElement standing;

        public Cell(int x, int y)
        {
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
        public IEnumerable<Element> Elements => elements;

        protected virtual void OnStandingElementChanged(Element standing)
        {
            StandingElementChanged?.Invoke(standing);
        }

        internal void AddElement(Element element)
        {
            elements.Add(element);
        }

        internal void RemoveElement(Element element)
        {
            elements.Remove(element);
        }

        public event Action<Element> StandingElementChanged;
    }
}