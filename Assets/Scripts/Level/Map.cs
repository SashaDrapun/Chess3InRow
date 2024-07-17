using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UI;
using Assets.Scripts.Level;
using Assets.Scripts.Menu;

namespace Assets.Scripts
{
    public class Map
    {
        public MapCellType[,] map;
        public const int SIZE = 8;
        public const int PIECES = 8;
        const int ADD_PIECES = 5;
        public bool[,] Mark;
        public int PiecesToDelete;
        private bool[,] counted;
        private bool[,] whereUserCanGo;
        private LevelProgress levelProgress;
        readonly ShowBox ShowBox;
        readonly ShowProgressOfTheLevel ShowProgressOfTheLevel;
        private static readonly Random random = new();
        Point fromPosition;
        bool isPieceSelected;
        FigureType typePieceSelected;

        public Map(ShowBox showBox, ShowProgressOfTheLevel showProgressOfTheLevel)
        {
            map = new MapCellType[SIZE, SIZE];
            ShowBox = showBox;
            this.ShowProgressOfTheLevel = showProgressOfTheLevel;
            whereUserCanGo = new bool[SIZE, SIZE];
            Mark = new bool[SIZE, SIZE];
            levelProgress = new LevelProgress();
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
            levelProgress.CountMoves++;
            
            CutLines();
            AddRandomPieces();
            ShowProgressOfTheLevel(levelProgress);
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
            typePieceSelected = GetPieceType(map[x,y]);
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
                        ShowBox(x, y, MapCellType.AllocatedSpace);
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

        private FigureType GetPieceType(MapCellType mapElement)
        {
            switch (mapElement)
            {
                case MapCellType.EmptyPlace:
                    return FigureType.Pawn;
                case MapCellType.Pawn:
                    return FigureType.Pawn;
                case MapCellType.Knight:
                    return FigureType.Knight;
                case MapCellType.Bishop:
                    return FigureType.Bishop;
                case MapCellType.Rook:
                    return FigureType.Rook;
                case MapCellType.Queen:
                    return FigureType.Queen;
                case MapCellType.King:
                    return FigureType.King;
                case MapCellType.AllocatedSpace:
                    return FigureType.Pawn;
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
                AddPiecesToStatistics();
                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        if (Mark[x, y])
                        {
                            SetMap(x, y, MapCellType.EmptyPlace);
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

        private void AddPiecesToStatistics()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (Mark[x, y])
                    {
                        switch (map[x, y])
                        {
                            case MapCellType.EmptyPlace:
                                break;
                            case MapCellType.Pawn:
                                levelProgress.CountCollectedPawns++;
                                break;
                            case MapCellType.Knight:
                                levelProgress.CountCollectedKnights++;
                                break;
                            case MapCellType.Bishop:
                                levelProgress.CountCollectedBishops++;
                                break;
                            case MapCellType.Rook:
                                levelProgress.CountCollectedRooks++;
                                break; 
                            case MapCellType.Queen:
                                levelProgress.CountCollectedQueens++;
                                break;
                            case MapCellType.King:
                                levelProgress.CountCollectedKings++;
                                break;
                            case MapCellType.AllocatedSpace:
                                break;
                            default:
                                break;
                        }
                    }
                }
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
                    MapCellType mapCellType = GetMap(x, y);
                    if (mapCellType == MapCellType.EmptyPlace) continue;
                    FigureType figureType = GetPieceType(mapCellType);
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

        public MapCellType GetMap(int x, int y)
        {
            if (!OnMap(x, y)) return MapCellType.NotOnMap;
            return map[x, y];
        }

        public static MapCellType GetMap(int x, int y, MapCellType[,] map)
        {
            if (!OnMap(x, y, map)) return MapCellType.NotOnMap;
            return map[x, y];
        }

        public static bool OnMap(int x, int y, MapCellType[,] map)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        public void SetMap(int x, int y, MapCellType mapElement)
        {
            map[x, y] = mapElement;
            ShowBox(x, y, mapElement);
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
            SetMap(x, y, (MapCellType)piece);
        }
    }
}
