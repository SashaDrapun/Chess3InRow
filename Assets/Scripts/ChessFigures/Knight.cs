using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ChessFigures
{
    public class Knight : Figure
    {
        public Knight(Point currentPosition) : base(currentPosition)
        {
        }

        public override List<Point> WhereCanMove(int[,] map)
        {
            List<Point> whereCanMove = new List<Point>
            {
                new Point(CurrentPosition.X + 2, CurrentPosition.Y - 1),
                new Point(CurrentPosition.X + 2, CurrentPosition.Y + 1),
                new Point(CurrentPosition.X - 2, CurrentPosition.Y - 1),
                new Point(CurrentPosition.X - 2, CurrentPosition.Y + 1),
                new Point(CurrentPosition.X - 1, CurrentPosition.Y + 2),
                new Point(CurrentPosition.X + 1, CurrentPosition.Y + 2),
                new Point(CurrentPosition.X - 1, CurrentPosition.Y - 2),
                new Point(CurrentPosition.X + 1, CurrentPosition.Y + 2)
            };

            List<Point> result = new List<Point>();

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y, map) == 0)
                {
                    result.Add(point);
                }
            }

            return result;
        }

        public override bool CanMove(Point toLocation, int[,] map)
        {
            List<Point> whereCanMove = WhereCanMove(map);

            if (whereCanMove.Contains(toLocation))
            {
                return true;
            }
            return false;
        }
    }
}
