using Assets.Scripts.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Levels.Education
{
    public class OnLoad : MonoBehaviour
    {
        private void Awake()
        { 
            DataManipulator dataManipulator = new DataManipulator();
            ApplicationData.MapInformation = dataManipulator.LoadMapInformation();
            LoadScene();
        }

        private void LoadScene()
        {
            SetStarsAndActiveFigures();
        }

        private void SetStarsAndActiveFigures()
        {
            SetRookStarsAndWings();
            SetBishopStarsAndWings();
            SetQueenStarsAndWings();
            SetKingStarsAndWings();
            SetKnightStarsAndWings();
            SetPawnStarsAndWings();
        }

        private void SetRookStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[0] >= 1)
            {
                SetPicture("RookStar1", "StarOn");
                SetPicture("Bishop", "BishopActive");
                SetTag("Bishop", "Bishop");
            }

            if ((int)ApplicationData.MapInformation.Levels[0] >= 2)
            {
                SetPicture("RookStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[0] >= 3)
            {
                SetPicture("RookStar3", "StarOn");
            }
        }

        private void SetBishopStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[1] >= 1)
            {
                SetPicture("BishopStar1", "StarOn");
                SetPicture("Queen", "QueenActive");
                SetTag("Queen", "Queen");
            }

            if ((int)ApplicationData.MapInformation.Levels[1] >= 2)
            {
                SetPicture("BishopStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[1] >= 3)
            {
                SetPicture("BishopStar3", "StarOn");
            }
        }

        private void SetQueenStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[2] >= 1)
            {
                SetPicture("QueenStar1", "StarOn");
                SetPicture("King", "KingActive");
                SetTag("King", "King");
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 2)
            {
                SetPicture("QueenStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 3)
            {
                SetPicture("QueenStar3", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 4)
            {

            }
        }

        private void SetKingStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[3] >= 1)
            {
                SetPicture("KingStar1", "StarOn");
                SetPicture("Knight", "KnightActive");
                SetTag("Knight", "Knight");
            }

            if ((int)ApplicationData.MapInformation.Levels[3] >= 2)
            {
                SetPicture("KingStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[3] >= 3)
            {
                SetPicture("KingStar3", "StarOn");
            }
        }

        private void SetKnightStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[4] >= 1)
            {
                SetPicture("KnightStar1", "StarOn");
                SetPicture("Pawn", "PawnActive");
                SetTag("Pawn", "Pawn");
            }

            if ((int)ApplicationData.MapInformation.Levels[4] >= 2)
            {
                SetPicture("KnightStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[4] >= 3)
            {
                SetPicture("KnightStar3", "StarOn");
            }
        }

        private void SetPawnStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[5] >= 1)
            {
                SetPicture("PawnStar1", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[5] >= 2)
            {
                SetPicture("PawnStar2", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[5] >= 3)
            {
                SetPicture("PawnStar3", "StarOn");
            }
        }

        private void SetPicture(string objectToSetPictureName, string objectFromSetPictureName)
        {
            Image objectToSetPicture = GameObject.Find(objectToSetPictureName).GetComponent<Image>();
            Image objectFromSetPicture = GameObject.Find(objectFromSetPictureName).GetComponent<Image>();

            objectToSetPicture.sprite = objectFromSetPicture.sprite;
        }

        private void SetTag(string objectToSetTagName, string tag)
        {
            GameObject objectToSetTag = GameObject.Find(objectToSetTagName);
            objectToSetTag.tag = tag;
        }
    }
}
