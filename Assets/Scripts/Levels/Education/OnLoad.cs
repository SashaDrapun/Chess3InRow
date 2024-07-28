using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
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
            LoadSprites();
            LoadScene();
        }

        private void LoadSprites()
        {
            starOnSprite = ObjectManager.GetSprite("StarOn");
        }

        Sprite starOnSprite;

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
                ObjectManager.SetPicture("RookStar1", starOnSprite);
                ObjectManager.SetPicture("Bishop", "BishopActive");
                ObjectManager.SetTag("Bishop", "Bishop");
            }

            if ((int)ApplicationData.MapInformation.Levels[0] >= 2)
            {
                ObjectManager.SetPicture("RookStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[0] >= 3)
            {
                ObjectManager.SetPicture("RookStar3", starOnSprite);
            }
        }

        private void SetBishopStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[1] >= 1)
            {
                ObjectManager.SetPicture("BishopStar1", starOnSprite);
                ObjectManager.SetPicture("Queen", "QueenActive");
                ObjectManager.SetTag("Queen", "Queen");
            }

            if ((int)ApplicationData.MapInformation.Levels[1] >= 2)
            {
                ObjectManager.SetPicture("BishopStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[1] >= 3)
            {
                ObjectManager.SetPicture("BishopStar3", starOnSprite);
            }
        }

        private void SetQueenStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[2] >= 1)
            {
                ObjectManager.SetPicture("QueenStar1", starOnSprite);
                ObjectManager.SetPicture("King", "KingActive");
                ObjectManager.SetTag("King", "King");
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 2)
            {
                ObjectManager.SetPicture("QueenStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 3)
            {
                ObjectManager.SetPicture("QueenStar3", "StarOn");
            }

            if ((int)ApplicationData.MapInformation.Levels[2] >= 4)
            {

            }
        }

        private void SetKingStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[3] >= 1)
            {
                ObjectManager.SetPicture("KingStar1", starOnSprite);
                ObjectManager.SetPicture("Knight", "KnightActive");
                ObjectManager.SetTag("Knight", "Knight");
            }

            if ((int)ApplicationData.MapInformation.Levels[3] >= 2)
            {
                ObjectManager.SetPicture("KingStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[3] >= 3)
            {
                ObjectManager.SetPicture("KingStar3", starOnSprite);
            }
        }

        private void SetKnightStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[4] >= 1)
            {
                ObjectManager.SetPicture("KnightStar1", starOnSprite);
                ObjectManager.SetPicture("Pawn", "PawnActive");
                ObjectManager.SetTag("Pawn", "Pawn");
            }

            if ((int)ApplicationData.MapInformation.Levels[4] >= 2)
            {
                ObjectManager.SetPicture("KnightStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[4] >= 3)
            {
                ObjectManager.SetPicture("KnightStar3", "StarOn");
            }
        }

        private void SetPawnStarsAndWings()
        {
            if ((int)ApplicationData.MapInformation.Levels[5] >= 1)
            {
                ObjectManager.SetPicture("PawnStar1", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[5] >= 2)
            {
                ObjectManager.SetPicture("PawnStar2", starOnSprite);
            }

            if ((int)ApplicationData.MapInformation.Levels[5] >= 3)
            {
                ObjectManager.SetPicture("PawnStar3", starOnSprite);
            }
        }
    }
}
