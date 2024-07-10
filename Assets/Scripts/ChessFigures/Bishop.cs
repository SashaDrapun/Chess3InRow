﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ChessFigures
{
    public class Bishop : Figure
    {
        public Bishop(Point currentPosition) : base(currentPosition)
        {
        }


        public override List<Point> WhereCanMove(int[,] map)
        {
            List<Point> result = new List<Point>();

            result.AddRange(WhereCanMoveByMap(CurrentPosition.X, CurrentPosition.Y, 1, 1, map));
            result.AddRange(WhereCanMoveByMap(CurrentPosition.X, CurrentPosition.Y, 1, -1, map));
            result.AddRange(WhereCanMoveByMap(CurrentPosition.X, CurrentPosition.Y, -1, 1, map));
            result.AddRange(WhereCanMoveByMap(CurrentPosition.X, CurrentPosition.Y, -1, -1, map));

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