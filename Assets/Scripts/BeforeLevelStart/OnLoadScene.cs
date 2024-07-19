using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BeforeLevelStart
{
    public class OnLoadScene : MonoBehaviour
    {
        private void OnEnable()
        {
            Image background = GameObject.Find($"Background").GetComponent<Image>();

            if (ApplicationData.SelectedLevel == "Pawn")
            {
                background.sprite = GameObject.Find($"BeforePawnLevelStart").GetComponent<Image>().sprite;
            }

            if (ApplicationData.SelectedLevel == "Knight")
            {
                background.sprite = GameObject.Find($"BeforePawnLevelStart").GetComponent<Image>().sprite;
            }

            if (ApplicationData.SelectedLevel == "Bishop")
            {
                background.sprite = GameObject.Find($"BeforePawnLevelStart").GetComponent<Image>().sprite;
            }

            if (ApplicationData.SelectedLevel == "Rook")
            {
                background.sprite = GameObject.Find($"BeforeRookLevelStart").GetComponent<Image>().sprite;
            }

            if (ApplicationData.SelectedLevel == "Queen")
            {
                background.sprite = GameObject.Find($"BeforeQueenLevelStart").GetComponent<Image>().sprite;
            }

            if (ApplicationData.SelectedLevel == "King")
            {
                background.sprite = GameObject.Find($"BeforeKingLevelStart").GetComponent<Image>().sprite;
            }
        }
    }
}
