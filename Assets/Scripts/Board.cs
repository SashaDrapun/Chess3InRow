using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public delegate void ShowBox(int x, int y, int piece);

    public class Board
    {
        public const int SIZE = 8;
        public const int PIECES = 7;
        const int ADD_PIECES = 5;
        private Random random = new Random();

        ShowBox ShowBox;

        int[,] map;
        Point fromPosition;
        bool isPieceSelected;
        FigureType typePieceSelected;

        public Board(ShowBox showBox)
        {
            ShowBox = showBox;
            map = new int[SIZE, SIZE];
        }

        public void Start()
        {
            ClearMap();
            AddRandomPieces();
            isPieceSelected = false;
        }

        public void Click(int x, int y)
        {
            if (map[x, y] > 0)
            {
                TakePiece(x, y);
            }
            else
            {
                MovePiece(x, y);
            }
        }

        private void MovePiece(int x, int y)
        {
            if (!isPieceSelected) return;
            if (!CanMove(new Point(x,y))) return;
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
            typePieceSelected = GetPieceType(x,y);
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
            MapExplore mapExplore = new(map);
            mapExplore.SetPiecesToCut();
            
            if (mapExplore.Balls > 0)
            {
                //playCut(); музыка
                for (int x = 0; x < SIZE; x++)
                {
                    for (int y = 0; y < SIZE; y++)
                    {
                        if (mapExplore.Mark[x, y])
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

        private bool OnMap(int x, int y)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        private int GetMap(int x, int y)
        {
            if (!OnMap(x, y)) return 0;
            return map[x, y];
        }



        private void SetMap(int x, int y, int piece)
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
            int piece = 1 + random.Next(PIECES - 1);
            SetMap(x, y, piece);
        }
    }
}