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

        public int CountMoveFor3Stars;
        public int CountMoveFor2Stars;
        public int CountMoveFor1Star;

        public GoalsOnTheLevel(Dictionary<FigureType, int> pieceToCollectAndCount, int countMoveFor3Stars, int countMoveFor2Stars, int countMoveFor1Stars)
        {
            PieceToCollectAndCount = pieceToCollectAndCount;
            CountMoveFor3Stars = countMoveFor3Stars;
            CountMoveFor2Stars = countMoveFor2Stars;
            CountMoveFor1Star = countMoveFor1Stars;
        }
    }
}
