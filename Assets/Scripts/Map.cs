using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Map
    {
        public int[,] map;
        public const int SIZE = 8;
        public const int PIECES = 8;
        const int ADD_PIECES = 5;
        public bool[,] Mark;
        public int PiecesToDelete;
        private bool[,] counted;
        private bool[,] whereUserCanGo;

        ShowBox ShowBox;
        private static Random random = new Random();
        Point fromPosition;
        bool isPieceSelected;
        FigureType typePieceSelected;

        public Map(ShowBox showBox)
        {
            map = new int[SIZE, SIZE];
            ShowBox = showBox;
            whereUserCanGo = new bool[SIZE, SIZE];
            Mark = new bool[SIZE, SIZE];
        }

        public void Refresh()
        {
            ClearMap();
            AddRandomPieces();
            isPieceSelected = false;
        }

        public void Click(int x, int y)
        {
            CleanWhereUserCanGo();
            if (map[x, y] > 0)
            {
                TakePiece(x, y);
            }
            else
            {
                MovePiece(x, y);
            }

            ShowWhereUserCanGo();
        }

        private void MovePiece(int x, int y)
        {
            if (!isPieceSelected) return;
            if (!CanMove(new Point(x, y))) return;
            SetMap(x, y, map[fromPosition.X, fromPosition.Y]);
            SetMap(fromPosition.X, fromPosition.Y, 0);
            isPieceSelected = false;

            CutLines();
            AddRandomPieces();
            //do
            //{
            //    AddRandomPieces();
            //}
            //while (CutLines());

        }

        private void TakePiece(int x, int y)
        {
            fromPosition.X = x;
            fromPosition.Y = y;
            isPieceSelected = true;
            typePieceSelected = GetPieceType(x, y);
            SetWhereUserCanGo();
        }

        private void CleanWhereUserCanGo()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (whereUserCanGo[x, y])
                    {
                        ShowBox(x, y, 0);
                    }
                }
            }

            whereUserCanGo = new bool[SIZE, SIZE];
        }

        private void ShowWhereUserCanGo()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (whereUserCanGo[x,y])
                    {
                        ShowBox(x, y, 7);
                    }
                }
            }
        }

        private void SetWhereUserCanGo()
        {
            Figure figure = FiguresFactory.CreateFigure(typePieceSelected, fromPosition);
            foreach (var placeToGo in figure.WhereCanMove(map))
            {
                whereUserCanGo[placeToGo.X, placeToGo.Y] = true;
            }
        }

        private FigureType GetPieceType(int x, int y)
        {
            int pieceType = map[x, y];
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

        private bool CutLines()
        {
            SetPiecesToCut();

            if (PiecesToDelete > 0)
            {
                //playCut(); музыка
                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        if (Mark[x, y])
                        {
                            SetMap(x, y, 0);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetPiecesToCut()
        {
            PiecesToDelete = 0;
            Mark = new bool[SIZE, SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    int piece = GetMap(x, y);
                    if (piece == 0) continue;
                    FigureType figureType = GetPieceType(x, y);
                    counted = new bool[SIZE, SIZE];
                    int countConnectedPieces = CalculateConnectedFigures(x, y, figureType);
                    if (countConnectedPieces >= 3)
                    {
                        PiecesToDelete += countConnectedPieces;
                        MarkLongRangeFigures(x, y, figureType);
                    }
                }
            }
        }

        private int CalculateConnectedFigures(int x0, int y0, FigureType figureType)
        {
            int piece = GetMap(x0, y0);
            Figure figure = FiguresFactory.CreateFigure(figureType, new Point(x0, y0));
            int count = 1;
            counted[x0, y0] = true;
            List<Point> connectedPieces = figure.ConnectedPieces(map);

            foreach (var connectedPiece in connectedPieces)
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
            List<Point> connectedPieces = figure.ConnectedPieces(map);

            foreach (var connectedPiece in connectedPieces)
            {
                if (Mark[connectedPiece.X, connectedPiece.Y]) continue;
                MarkLongRangeFigures(connectedPiece.X, connectedPiece.Y, figureType);
            }
        }

        private bool CanMove(Point toLocation)
        {
            Figure figure = FiguresFactory.CreateFigure(typePieceSelected, fromPosition);
            return figure.CanMove(toLocation, map);
        }

        private void ClearMap()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    SetMap(x, y, 0);
                }
            }
        }

        public bool OnMap(int x, int y)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        public int GetMap(int x, int y)
        {
            if (!OnMap(x, y)) return -1;
            return map[x, y];
        }

        public static int GetMap(int x, int y, int[,] map)
        {
            if (!OnMap(x, y, map)) return -1;
            return map[x, y];
        }

        public static bool OnMap(int x, int y, int[,] map)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        public void SetMap(int x, int y, int piece)
        {
            map[x, y] = piece;
            ShowBox(x, y, piece);
        }

        private void AddRandomPieces()
        {
            for (int j = 0; j < ADD_PIECES; j++)
            {
                AddRandomPiece();
            }
        }

        private void AddRandomPiece()
        {
            int x, y;
            int loop = SIZE * SIZE;
            do
            {
                x = random.Next(SIZE);
                y = random.Next(SIZE);
                if (--loop <= 0) return;
            }
            while (map[x, y] > 0);
            int piece = 1 + random.Next(PIECES - 2);
            SetMap(x, y, piece);
        }
    }
}
