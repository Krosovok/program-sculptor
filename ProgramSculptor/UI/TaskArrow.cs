using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UI
{
    class TaskArrow : Shape
    {
        private const int H = 20;
        private const int W = 100;
        private const int HalfH = H / 2;

        private static readonly Point[] Path =
        {
            new Point(0, 0),
            new Point(HalfH, HalfH),
            new Point(0, H),

            new Point(W - HalfH, H),
            new Point(W, HalfH),
            new Point(W - HalfH, 0),
        };
        private static Geometry arrowGeometry;

        public TaskArrow()
        {
            this.Stretch = Stretch.Fill;
        }

        protected override Geometry DefiningGeometry => GetGeometry();

        private static Geometry GetGeometry()
        {
            if (arrowGeometry == null)
            {
                arrowGeometry = CreateGeometry();
            }

            return arrowGeometry;
        }

        private static Geometry CreateGeometry()
        {
            IEnumerable<LineSegment> pathSegments =
                Path.Skip(1).Select(point => new LineSegment(point, true));
            PathFigure[] figures = { new PathFigure(Path[0], pathSegments, true) };
            return new PathGeometry(figures);
        }
    }
    
}
