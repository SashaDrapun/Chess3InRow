using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class MapExplore
    {
        int[,] Map;
        public bool[,] Mark;
        public int Balls;
        private bool[,] counted;
        public MapExplore(int[,] map)
        {
            this.Map = map;
            Mark = new bool[Board.SIZE, Board.SIZE];
            counted = new bool[Board.SIZE, Board.SIZE];
            Balls = 0;
        }

        public void SetPiecesToCut()
        {
            Mark = new bool[Board.SIZE, Board.SIZE];
            for (int x = 0; x < Board.SIZE; x++)
            {
                for (int y = 0; y < Board.SIZE; y++)
                {
                    int piece = GetMap(x, y);
                    if (piece == 0) continue;
                    FigureType figureType = GetPieceType(x, y);
                    counted = new bool[Board.SIZE, Board.SIZE];
                    int countConnectedPieces = CalculateConnectedFigures(x, y, figureType);
                    if (countConnectedPieces >= 3)
                    {
                        Balls += countConnectedPieces;
                        MarkLongRangeFigures(x, y, figureType);
                    }
                }
            }
        }

        private int CutPawn(Point currentPosition)
        {
            List<Point> whereCanMove = new List<Point>
            {
                new Point(currentPosition.X + 1, currentPosition.Y + 1),
                new Point(currentPosition.X -1, currentPosition.Y + 1)
            };

            int count = 0;
            List<Point> result = new List<Point>();

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 1)
                {
                    count++;
                }
            }

            if (count < 3)
            {
                return 0;
            }

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 1)
                {
                    Mark[point.X, point.Y] = true;
                }
            }

            return count;
        }

        private int CutKnight(Point currentPosition)
        {
            Debug.Print("CutKnight");
            List<Point> whereCanMove = new List<Point>
            {
                new Point(currentPosition.X + 2, currentPosition.Y - 1),
                new Point(currentPosition.X + 2, currentPosition.Y + 1),
                new Point(currentPosition.X - 2, currentPosition.Y - 1),
                new Point(currentPosition.X - 2, currentPosition.Y + 1),
                new Point(currentPosition.X - 1, currentPosition.Y + 2),
                new Point(currentPosition.X + 1, currentPosition.Y + 2),
                new Point(currentPosition.X - 1, currentPosition.Y - 2),
                new Point(currentPosition.X + 1, currentPosition.Y + 2)
            };

            int count = 0;
            List<Point> result = new List<Point>();

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 2)
                {
                    count++;
                }
            }

            if (count < 3)
            {
                return 0;
            }

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 2)
                {
                    Mark[point.X, point.Y] = true;
                }
            }

            return count;
        }

        private int CutKing(Point currentPosition)
        {
            Debug.Print("CutKing");
            List<Point> whereCanMove = new List<Point>
            {
                new Point(currentPosition.X + 1, currentPosition.Y),
                new Point(currentPosition.X - 1, currentPosition.Y),
                new Point(currentPosition.X, currentPosition.Y + 1),
                new Point(currentPosition.X, currentPosition.Y - 1),
                new Point(currentPosition.X + 1, currentPosition.Y + 1),
                new Point(currentPosition.X + 1, currentPosition.Y - 1),
                new Point(currentPosition.X - 1, currentPosition.Y + 1),
                new Point(currentPosition.X - 1, currentPosition.Y - 1)
            };

            List<Point> result = new List<Point>();
            int count = 0;

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 6)
                {
                    count++;
                }
            }

            if (count < 3)
            {
                return 0;
            }

            foreach (Point point in whereCanMove)
            {
                if (GetMap(point.X, point.Y) == 6)
                {
                    Mark[point.X, point.Y] = true;
                }
            }

            return count;
        }

        private int CalculateConnectedFigures(int x0, int y0, FigureType figureType)
        {
            int piece = GetMap(x0, y0);
            Figure figure = FiguresFactory.CreateFigure(figureType, new Point(x0, y0));
            int count = 1;
            counted[x0, y0] = true;
            List<Point> connectedPieces = figure.ConnectedPieces(Map);

            foreach(var connectedPiece in connectedPieces)
            {
                if (counted[connectedPiece.X, connectedPiece.Y]) continue;
                count += CalculateConnectedFigures(connectedPiece.X, connectedPiece.Y, figureType);
            }

            return count;
        }

        private void MarkLongRangeFigures(int x0, int y0, FigureType figureType)
        {
            int piece = GetMap(x0, y0);
            Figure figure = FiguresFactory.CreateFigure(figureType, new Point(x0, y0));
            Mark[x0, y0] = true;
            List<Point> connectedPieces = figure.ConnectedPieces(Map);

            foreach (var connectedPiece in connectedPieces)
            {
                if (Mark[connectedPiece.X, connectedPiece.Y]) continue;
                MarkLongRangeFigures(connectedPiece.X, connectedPiece.Y, figureType);
            }
        }

        private bool OnMap(int x, int y)
        {
            return x >= 0 && x < Board.SIZE && y >= 0 && y < Board.SIZE;
        }

        private int GetMap(int x, int y)
        {
            if (!OnMap(x, y)) return -1;
            return Map[x, y];
        }

        private FigureType GetPieceType(int x, int y)
        {
            int pieceType = Map[x, y];

            switch (pieceType)
            {
                case 1:
                    return FigureType.Pawn;
                case 2:
                    return FigureType.Knight;
                case 3:
                    return FigureType.Bishop;
                case 4:
                    return FigureType.Rook;
                case 5:
                    return FigureType.Queen;
                case 6:
                    return FigureType.King;
                default:
                    break;
            }
            return FigureType.Pawn;
        }
    }
}
