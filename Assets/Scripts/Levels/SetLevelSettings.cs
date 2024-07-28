using Assets.Scripts.ChessFigures;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Levels
{
    public static class SetLevelSettings
    {
        public static void SetFiguresAvailableOnLevel()
        {
            ApplicationData.FiguresAvailableOnLevel = new List<FigureType>();
            if (ApplicationData.SelectedLevel == "Pawn")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.Pawn);
            }

            if (ApplicationData.SelectedLevel == "Knight")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.Knight);
            }

            if (ApplicationData.SelectedLevel == "Bishop")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.Bishop);
            }

            if (ApplicationData.SelectedLevel == "Rook")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.Rook);
            }

            if (ApplicationData.SelectedLevel == "Queen")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.Queen);
            }

            if (ApplicationData.SelectedLevel == "King")
            {
                ApplicationData.FiguresAvailableOnLevel.Add(FigureType.King);
            }

            if (ApplicationData.SelectedLevel == "Standart")
            {
                SetAllFigureAvailableOnLevel();
            }
        }

        public static void SetGoalsOnLevel()
        {
            Dictionary<FigureType, int> pieceToCollectAndCount = new Dictionary<FigureType, int>();

            if (ApplicationData.SelectedLevel == "Pawn")
            {
                pieceToCollectAndCount.Add(FigureType.Pawn, 150);
                ApplicationData.CurrentLevel = 5;
            }

            if (ApplicationData.SelectedLevel == "Knight")
            {
                pieceToCollectAndCount.Add(FigureType.Knight, 30);
                ApplicationData.CurrentLevel = 4;
            }

            if (ApplicationData.SelectedLevel == "Bishop")
            {
                pieceToCollectAndCount.Add(FigureType.Bishop, 30);
                ApplicationData.CurrentLevel = 1;
            }

            if (ApplicationData.SelectedLevel == "Rook")
            {
                pieceToCollectAndCount.Add(FigureType.Rook, 30);
                ApplicationData.CurrentLevel = 0;
            }

            if (ApplicationData.SelectedLevel == "Queen")
            {
                pieceToCollectAndCount.Add(FigureType.Queen, 30);
                ApplicationData.CurrentLevel = 2;
            }

            if (ApplicationData.SelectedLevel == "King")
            {
                pieceToCollectAndCount.Add(FigureType.King, 30);
                ApplicationData.CurrentLevel = 3;
            }

            if (ApplicationData.SelectedLevel == "Standart")
            {
                SetAllFigureAvailableOnLevel();
            }

            ApplicationData.GoalsOnTheLevel = new GoalsOnTheLevel(pieceToCollectAndCount, 10, 15, 20);
        }

        private static void SetAllFigureAvailableOnLevel()
        {
            ApplicationData.FiguresAvailableOnLevel = new List<FigureType>
            {
                FigureType.Pawn,
                FigureType.Knight,
                FigureType.Bishop,
                FigureType.Rook,
                FigureType.Queen,
                FigureType.King
            };
        }
    }
}
