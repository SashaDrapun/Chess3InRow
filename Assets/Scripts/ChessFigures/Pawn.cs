using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ChessFigures
{
    public class Pawn : Figure
    {
        public Pawn(Point currentPosition) : base(currentPosition)
        {
        }

        public override List<Point> WhereCanMove(int[,] map)
        {
            List<Point> result = new List<Point>();
            if (GetMap(CurrentPosition.X, CurrentPosition.Y - 1, map) == 0)
            {
                result.Add(new Point(CurrentPosition.X, CurrentPosition.Y - 1));
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
