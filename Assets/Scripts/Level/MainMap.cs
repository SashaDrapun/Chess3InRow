﻿using Assets.Scripts.ChessFigures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Assets.Scripts.Level;
using Assets.Scripts.Shop;
using Assets.Scripts.DataService;

namespace Assets.Scripts
{
    public class MainMap
    {
        public MapCellType[,] map;
        public const int SIZE = 8;
        public const int PIECES = 8;
        private const int ADD_PIECES = 3;
        public bool[,] Mark;
        public int PiecesToDelete;
        private bool[,] counted;
        private bool[,] whereUserCanGo;
        private LevelProgress levelProgress;

        readonly ShowBox ShowBox;
        readonly ShowProgressOfTheLevel ShowProgressOfTheLevel;
        readonly GetRandomFigureFromAvailable GetRandomFigureFromAvailable;

        private static readonly System.Random random = new();
        private Point fromPosition;
        private bool isPieceSelected;
        private FigureType typePieceSelected;

        private MonoBehaviour coroutineRunner;

        public MainMap(ShowBox showBox, ShowProgressOfTheLevel showProgressOfTheLevel,
                        GetRandomFigureFromAvailable getRandomFigureFromAvailable, MonoBehaviour coroutineRunner)
        {
            map = new MapCellType[SIZE, SIZE];
            ShowBox = showBox;
            GetRandomFigureFromAvailable = getRandomFigureFromAvailable;
            this.ShowProgressOfTheLevel = showProgressOfTheLevel;
            whereUserCanGo = new bool[SIZE, SIZE];
            Mark = new bool[SIZE, SIZE];
            levelProgress = new LevelProgress();
            this.coroutineRunner = coroutineRunner;
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

        public void UseBonus(ShopItem shopItem)
        {
            if (shopItem == ShopItem.Teleporter)
            {
                TeleportItems();
            }

            if (shopItem == ShopItem.Redistributor)
            {
                
                RedistributeItems();
            }
        }

        private void RedistributeItems()
        {
            int countPieces = GetCountItemsOnMap();
            this.ClearMap();
            AddRandomPieces(countPieces);
        }

        private void TeleportItems()
        {
            List<MapCellType> itemsOnMap = GetItemsOnMap();
            this.ClearMap();
            foreach (var mapItem in itemsOnMap)
            {
                AddMapElement(mapItem);
            }
        }

        private List<MapCellType> GetItemsOnMap()
        {
            List<MapCellType> result = new List<MapCellType>();
            
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (map[x, y] != MapCellType.NotOnMap && map[x, y] != MapCellType.AllocatedSpace && map[x, y] != MapCellType.EmptyPlace)
                    {
                        result.Add(map[x, y]);
                    }
                }
            }

            return result;
        }

        private int GetCountItemsOnMap()
        {
            int count = 0;

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (map[x, y] != MapCellType.NotOnMap && map[x, y] != MapCellType.AllocatedSpace && map[x, y] != MapCellType.EmptyPlace)
                    {
                        count++;
                    }
                }
            }

            return count;
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

            coroutineRunner.StartCoroutine(AddRandomPiecesAndCheckLines());
        }

        private IEnumerator AddRandomPiecesAndCheckLines()
        {
            do
            {
                AddRandomPieces();
                ShowProgressOfTheLevel(levelProgress);
                yield return new WaitForSeconds(0.5f); // Задержка на 1 секунду
            }
            while (CutLines());

            
        }

        private void TakePiece(int x, int y)
        {
            fromPosition.X = x;
            fromPosition.Y = y;
            isPieceSelected = true;
            typePieceSelected = GetPieceType(map[x, y]);
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
                    if (whereUserCanGo[x, y])
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
            return mapElement switch
            {
                MapCellType.EmptyPlace => FigureType.Pawn,
                MapCellType.Pawn => FigureType.Pawn,
                MapCellType.Knight => FigureType.Knight,
                MapCellType.Bishop => FigureType.Bishop,
                MapCellType.Rook => FigureType.Rook,
                MapCellType.Queen => FigureType.Queen,
                MapCellType.King => FigureType.King,
                MapCellType.AllocatedSpace => FigureType.Pawn,
                _ => FigureType.Pawn,
            };
        }

        private MapCellType GetMapCellFromFigure(FigureType figureType)
        {
            return figureType switch
            {
                FigureType.Pawn => MapCellType.Pawn,
                FigureType.Knight => MapCellType.Knight,
                FigureType.Bishop => MapCellType.Bishop,
                FigureType.Rook => MapCellType.Rook,
                FigureType.Queen => MapCellType.Queen,
                FigureType.King => MapCellType.King,
                _ => MapCellType.Pawn,
            };
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

        private void AddRandomPieces(int count)
        {
            for (int j = 0; j < count; j++)
            {
                AddRandomPiece();
            }
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
            Point randomPlace = GetRandomEmptyPlace();
            FigureType randomFigure = GetRandomFigureFromAvailable();
            MapCellType piece = GetMapCellFromFigure(randomFigure);

            SetMap(randomPlace.X, randomPlace.Y, piece);
        }

        private void AddMapElement(MapCellType mapCellType)
        {
            Point randomPlace = GetRandomEmptyPlace();

            SetMap(randomPlace.X, randomPlace.Y, mapCellType);
        }

        private Point GetRandomEmptyPlace()
        {
            List<Point> emptyPlaces = new();
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (map[x, y] == MapCellType.EmptyPlace || map[x, y] == MapCellType.AllocatedSpace)
                    {
                        emptyPlaces.Add(new Point(x, y));
                    }
                }
            }

            int randomPosition = random.Next(0, emptyPlaces.Count);

            return emptyPlaces[randomPosition];
        }
    }
}
