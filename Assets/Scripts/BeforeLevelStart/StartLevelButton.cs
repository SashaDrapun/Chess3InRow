using Assets.Scripts.ChessFigures;
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
