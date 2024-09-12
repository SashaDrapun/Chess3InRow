using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using Assets.Scripts.LevelSettingsFolder;
using Assets.Scripts.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Level
{
    public class LevelSceneObjectManipulator
    {
        private static readonly LevelSceneObjectManipulator instance = new LevelSceneObjectManipulator();

        private LevelSceneObjectManipulator() { }


        private const string GoalPicturePrefix = "GoalPicture";
        private const string GoalTextPrefix = "GoalText";
        private const string MovesText = "Moves";
        private const string GoalMovesText = "GoalMoves";

        public static LevelSceneObjectManipulator GetInstance()
        {
            return instance;
        }

        public static void SetScene(LevelSettings levelSettings)
        {
            LevelSceneObjectManipulator levelSceneObjectManipulator = GetInstance();
            levelSceneObjectManipulator.SetGoals(levelSettings);
        }

        public static void SetBonuses()
        {
            for (int i = 0; i < ApplicationData.ShopInformation.CountShopItems.Count; i++)
            {
                BonusType currentBonus = (BonusType)(i + 1);
                string objectCountName = $"{currentBonus}Count";
                
                int countBonuses = ApplicationData.ShopInformation.CountShopItems[i];

                if (!IsBonusActive(currentBonus))
                {
                    string objectName = $"{currentBonus}";
                    ObjectManager.SetPicture(objectName, $"{objectName}NonActive");
                }

                string outputCount = countBonuses.ToString();
                ObjectManager.OutputInformation(objectCountName, outputCount);
            }
        }


        private void SetGoals(LevelSettings levelSettings)
        {
            int goalNumber = 1;

            foreach (var keyValuePair in levelSettings.PieceToCollectAndCount)
            {
                string goalImageName = $"{GoalPicturePrefix}{goalNumber}";
                string pictureName = keyValuePair.Key.ToString();
                string goalTextName = $"{GoalTextPrefix}{goalNumber++}";

                ObjectManager.FindHiddenObjectAndSetActive(goalImageName);
                ObjectManager.FindHiddenObjectAndSetActive(goalTextName);
                ObjectManager.SetPicture(goalImageName, pictureName);

                ObjectManager.OutputInformation(goalTextName, $"0/{levelSettings.PieceToCollectAndCount[keyValuePair.Key]}");
            }

            ObjectManager.OutputInformation(MovesText, "0");
            ObjectManager.OutputInformation(GoalMovesText, levelSettings.CountMoveFor3Stars.ToString());
        }

        private static bool IsBonusActive(BonusType bonus)
        {
            if (ApplicationData.CurrentLevelMode == LevelMode.Usual && bonus == BonusType.Freezing) return false;
            if (ApplicationData.CurrentLevelMode == LevelMode.Silver && bonus == BonusType.Adder) return false;
            if (ApplicationData.ShopInformation.CountShopItems[(int)bonus-1] == 0) return false;

            return true;
        }
    }
}
