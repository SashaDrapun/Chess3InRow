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
            return figureType switch
            {
                FigureType.Pawn => new Pawn(currentPosition),
                FigureType.Knight => new Knight(currentPosition),
                FigureType.Bishop => new Bishop(currentPosition),
                FigureType.Rook => new Rook(currentPosition),
                FigureType.Queen => new Queen(currentPosition),
                FigureType.King => new King(currentPosition),
                _ => throw new ArgumentException("Invalid enum value", nameof(figureType)),
            };
        }
    }
}
