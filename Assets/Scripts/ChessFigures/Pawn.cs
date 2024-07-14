﻿using System;
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

        public override List<Point> WhereCanMove(int[,] map)
        {
            List<Point> whereCanMove = WhereCanMove();
            List<Point> result = new List<Point>();
            
            foreach (Point point in whereCanMove)
            {
                if (Map.GetMap(point.X, point.Y, map) == 0)
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

        public override List<Point> ConnectedPieces(int[,] map)
        {
            List<Point> whereCanShoot = WhereCanShoot();
            List<Point> result = new List<Point>();

            foreach (Point point in whereCanShoot)
            {
                if (Map.GetMap(point.X, point.Y, map) == 1)
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
                new Point(CurrentPosition.X, CurrentPosition.Y - 1)
            };
        }

        private List<Point> WhereCanShoot()
        {
            return new List<Point>
            {
                new Point(CurrentPosition.X + 1, CurrentPosition.Y - 1),
                new Point(CurrentPosition.X - 1, CurrentPosition.Y - 1)
            };
        }
    }
}
