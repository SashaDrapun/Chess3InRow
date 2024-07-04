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

        public override bool CanMove(Point toLocation, int[,] map)
        {
            throw new NotImplementedException();
        }

        public override List<Point> WhereCanMove(int[,] map)
        {
            throw new NotImplementedException();
        }
    }
}
