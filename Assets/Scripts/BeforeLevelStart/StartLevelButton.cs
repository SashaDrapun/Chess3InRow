using Assets.Scripts.ChessFigures;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.BeforeLevelStart
{
    public class StartLevelButton : MonoBehaviour
    {
        private void OnEnable()
        {
            GameObject.Find($"StartLevelButton").GetComponent<Button>().onClick.AddListener(OnStartLevelButtonClick);
        }

        public void OnStartLevelButtonClick()
        {
            SetFiguresAvailableOnLevel();
            SetGoalsOnLevel();
            SceneManager.LoadScene(3);
        }

        private void SetFiguresAvailableOnLevel()
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

        private void SetGoalsOnLevel()
        {
            int countMoves = 0;
            Dictionary<FigureType, int> pieceToCollectAndCount = new Dictionary<FigureType, int>();

            if (ApplicationData.SelectedLevel == "Pawn")
            {
                countMoves = 30;
                pieceToCollectAndCount.Add(FigureType.Pawn, 150);
            }

            if (ApplicationData.SelectedLevel == "Knight")
            {
                countMoves = 10;
                pieceToCollectAndCount.Add(FigureType.Knight, 30);
            }

            if (ApplicationData.SelectedLevel == "Bishop")
            {
                countMoves = 10;
                pieceToCollectAndCount.Add(FigureType.Bishop, 30);
            }

            if (ApplicationData.SelectedLevel == "Rook")
            {
                countMoves = 10;
                pieceToCollectAndCount.Add(FigureType.Rook, 30);
            }

            if (ApplicationData.SelectedLevel == "Queen")
            {
                countMoves = 10;
                pieceToCollectAndCount.Add(FigureType.Queen, 30);
            }

            if (ApplicationData.SelectedLevel == "King")
            {
                countMoves = 10;
                pieceToCollectAndCount.Add(FigureType.King, 30);
            }

            if (ApplicationData.SelectedLevel == "Standart")
            {
                SetAllFigureAvailableOnLevel();
            }

            ApplicationData.GoalsOnTheLevel = new GoalsOnTheLevel(pieceToCollectAndCount, 10,15,20);
        }

        private void SetAllFigureAvailableOnLevel()
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
