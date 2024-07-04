using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ChessFigures
{
    public static class FiguresFactory
    {
        public static Figure CreateFigure(FigureType figureType, Point currentPosition)
        {
            switch (figureType)
            {
                case FigureType.Pawn:
                    return new Pawn(currentPosition);
                case FigureType.Knight:
                    return new Knight(currentPosition);
                case FigureType.Bishop:
                    return new Bishop(currentPosition);
                case FigureType.Rook:
                    return new Rook(currentPosition);
                case FigureType.Queen:
                    return new Queen(currentPosition);
                default:
                    throw new ArgumentException("Invalid enum value", nameof(currentPosition));
            }
        }
    }
}
