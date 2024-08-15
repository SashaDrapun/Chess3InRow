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
            ApplicationData.MapInformation = DataManipulator.LoadMapInformation();
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
            var rookLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.Rook];

            if (rookLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("RookStar1", starOnSprite);
                ObjectManager.SetPicture("Bishop", "BishopActive");
                ObjectManager.SetTag("Bishop", "1");
            }

            if (rookLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("RookStar2", starOnSprite);
            }

            if (rookLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("RookStar3", starOnSprite);
            }

            if (rookLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("RookWings");
            }

            if (rookLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("RookWings", "WingsGolden");
            }
        }

        private void SetBishopStarsAndWings()
        {
            var bishopLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.Bishop];

            if (bishopLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("BishopStar1", starOnSprite);
                ObjectManager.SetPicture("Queen", "QueenActive");
                ObjectManager.SetTag("Queen", "2");
            }

            if (bishopLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("BishopStar2", starOnSprite);
            }

            if (bishopLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("BishopStar3", starOnSprite);
            }

            if (bishopLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("BishopWings");
            }

            if (bishopLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("BishopWings", "WingsGolden");
            }
        }

        private void SetQueenStarsAndWings()
        {
            var queenLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.Queen];

            if (queenLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("QueenStar1", starOnSprite);
                ObjectManager.SetPicture("King", "KingActive");
                ObjectManager.SetTag("King", "3");
            }

            if (queenLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("QueenStar2", starOnSprite);
            }

            if (queenLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("QueenStar3", "StarOn");
            }

            if (queenLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("QueenWings");
            }

            if (queenLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("QueenWings", "WingsGolden");
            }
        }

        private void SetKingStarsAndWings()
        {
            var kingLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.King];

            if (kingLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("KingStar1", starOnSprite);
                ObjectManager.SetPicture("Knight", "KnightActive");
                ObjectManager.SetTag("Knight", "4");
            }

            if (kingLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("KingStar2", starOnSprite);
            }

            if (kingLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("KingStar3", starOnSprite);
            }

            if (kingLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("KingWings");
            }

            if (kingLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("KingWings", "WingsGolden");
            }
        }

        private void SetKnightStarsAndWings()
        {
            var knightLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.Knight];

            if (knightLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("KnightStar1", starOnSprite);
                ObjectManager.SetPicture("Pawn", "PawnActive");
                ObjectManager.SetTag("Pawn", "5");
            }

            if (knightLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("KnightStar2", starOnSprite);
            }

            if (knightLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("KnightStar3", starOnSprite);
            }

            if (knightLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("KnightWings");
            }

            if (knightLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("KnightWings", "WingsGolden");
            }
        }

        private void SetPawnStarsAndWings()
        {
            var pawnLevelStatus = ApplicationData.MapInformation.Levels[(int)FigureTypeByEducationLevel.Pawn];

            if (pawnLevelStatus >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("PawnStar1", starOnSprite);
            }

            if (pawnLevelStatus >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("PawnStar2", starOnSprite);
            }

            if (pawnLevelStatus >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("PawnStar3", starOnSprite);
            }

            if (pawnLevelStatus >= LevelStatus.SilverWings)
            {
                ObjectManager.FindHiddenObjectAndSetActive("PawnWings");
            }

            if (pawnLevelStatus >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("PawnWings", "WingsGolden");
            }
        }
    }
}
