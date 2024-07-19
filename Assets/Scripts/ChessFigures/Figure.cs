using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Drawing;
using Assets.Scripts;
using Assets.Scripts.ChessFigures;

public abstract class Figure
{
    public Point CurrentPosition { get; set; }

    public Figure(Point currentPosition)
    {
        this.CurrentPosition = currentPosition;
    }

    public List<Point> WhereCanMoveByMap(int x0, int y0, int sx, int sy, MapCellType[,] map)
    {
        List<Point> result = new();

        for (int x = x0 + sx, y = y0 + sy; MainMap.GetMap(x, y, map) == (int)MapCellType.EmptyPlace; x += sx, y += sy)
        {
            result.Add(new Point(x, y));
        }

        return result;
    }

    public List<Point> FindConnectedPiecesForLongRangeFigures(int x0, int y0, int sx, int sy, MapCellType[,] map)
    {
        List<Point> result = new();
        MapCellType piece = MainMap.GetMap(x0, y0, map);

        for (int x = x0 + sx, y = y0 + sy; (MainMap.GetMap(x, y, map) == (int)MapCellType.EmptyPlace) || (MainMap.GetMap(x, y, map) == piece); x += sx, y += sy)
        {
            if (MainMap.GetMap(x, y, map) == piece)
            {
                result.Add(new Point(x, y));
                break;
            } 
        }

        return result;
    }

    public abstract bool CanMove(Point toLocation, MapCellType[,] map);

    public abstract List<Point> WhereCanMove(MapCellType[,] map);

    public abstract List<Point> ConnectedPieces(MapCellType[,] map);
}
