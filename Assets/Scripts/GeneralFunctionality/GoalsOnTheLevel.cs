using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GeneralFunctionality
{
    public class GoalsOnTheLevel
    {
        public Dictionary<FigureType, int> PieceToCollectAndCount;

        public int CountMoves;

        public GoalsOnTheLevel(int countMoves)
        {
            CountMoves = countMoves;
            PieceToCollectAndCount = new Dictionary<FigureType, int>();
        }

        public GoalsOnTheLevel(Dictionary<FigureType, int> pieceToCollectAndCount, int countMoves)
        {
            PieceToCollectAndCount = pieceToCollectAndCount;
            CountMoves = countMoves;
        }
    }
}
