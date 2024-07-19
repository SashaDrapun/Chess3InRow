using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.ChessFigures
{
    public class Pawn : Figure
    {
        public Pawn(Point currentPosition) : base(currentPosition)
        {
        }

        public override List<Point> WhereCanMove(MapCellType[,] map)
        {
            List<Point> whereCanMove = WhereCanMove();
            List<Point> result = new();
            
            foreach (Point point in whereCanMove)
            {
                if (MainMap.GetMap(point.X, point.Y, map) == MapCellType.EmptyPlace)
                {
                    result.Add(point);
                }
            }

            return result;
        }

        public override bool CanMove(Point toLocation, MapCellType[,] map)
        {
            List<Point> whereCanMove = WhereCanMove(map);

            if (whereCanMove.Contains(toLocation))
            {
                return true;
            }
            return false;
        }

        public override List<Point> ConnectedPieces(MapCellType[,] map)
        {
            List<Point> whereCanShoot = WhereCanShoot();
            List<Point> result = new();

            foreach (Point point in whereCanShoot)
            {
                if (MainMap.GetMap(point.X, point.Y, map) == MapCellType.Pawn)
                {
                    result.Add(point);
                }
            }

            return result;
        }

        private List<Point> WhereCanMove()
        {
            return new List<Point>
            {
                new(CurrentPosition.X, CurrentPosition.Y - 1)
            };
        }

        private List<Point> WhereCanShoot()
        {
            return new List<Point>
            {
                new(CurrentPosition.X + 1, CurrentPosition.Y - 1),
                new(CurrentPosition.X - 1, CurrentPosition.Y - 1)
            };
        }
    }
}
