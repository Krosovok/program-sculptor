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
        
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public NonPassableElement Standing { get; internal set; }
        public bool IsFree => Standing == null;
        public IEnumerable<Element> Elements => elements;

        internal void AddElement(Element element)
        {
            elements.Add(element);
        }
        
        internal void RemoveElement(Element element)
        {
            elements.Remove(element);
        }
    }
}