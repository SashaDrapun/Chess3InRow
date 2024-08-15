using Assets.Scripts.ChessFigures;
using Assets.Scripts.DataService;
using Assets.Scripts.Level;
using Assets.Scripts.LevelSettingsFolder;
using Assets.Scripts;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.GeneralFunctionality;

public class Game : MonoBehaviour
{
    public LevelManager levelManager;
    public TimerController timerController;
    public AnimationController animationController;
    public CoroutineRunner coroutineRunner;

    private LevelRewardSystem levelRewardSystem;
    private LevelSettings levelSettings;
    private static readonly System.Random random = new();

    private Button[,] buttons;
    private Image[] images;
    private Board board;
    private Image starOff;
    private Image firstStar;
    private Image secondStar;
    private Image thirdStar;

    private int countStars = 3;
    private int countMoves = 0;

    private const string GoalPicturePrefix = "GoalPicture";
    private const string GoalTextPrefix = "GoalText";
    private const string StarImagePrefix = "Star";
    private const string StarOnImage = "StarOn";
    private const string StarOffImage = "StarOff";
    private const string LvlCompletedObject = "LVLCompleted";
    private const string LvlFailedObject = "LVLFailed";
    private const string StarsObject = "Stars";
    private const string InfinitySymbolObject = "InfinitySymbol";
    private const string TimerObject = "Timer";
    private const string WingsObject = "Wings";
    private const string GoldenWingsImage = "GoldenWings";
    private const string EarnedMoneyText = "EarnedMoney";
    private const string MovesText = "Moves";
    private const string GoalMovesText = "GoalMoves";

    void Start()
    {
        LoadLevelSettings();
        InitButtons();
        InitImages();
        SetGoals();

        this.board = new Board(ShowBox, ShowStatistics, GetRandomFigureFromAvailable, coroutineRunner);
        board.Start();

        SetElements();
        levelRewardSystem = new LevelRewardSystem();
        timerController.OnTimerEnded += OnTimerEnded;
    }

    private void OnTimerEnded()
    {
        Lose();
    }

    private void LoadLevelSettings()
    {
        levelSettings = levelManager.GetLevelSettings(ApplicationData.CurrentLevel);
    }

    private void SetElements()
    {
        if (ApplicationData.CurrentLevelMode >= LevelMode.Silver)
        {
            ObjectManager.SetObjectNonActive(StarsObject);
            ObjectManager.SetObjectNonActive(InfinitySymbolObject);
            ObjectManager.FindHiddenObjectAndSetActive(TimerObject);
        }

        if (ApplicationData.CurrentLevelMode == LevelMode.Silver)
        {
            timerController.StartTimer(levelSettings.CountSecondsForSilverDificulty);
        }

        if (ApplicationData.CurrentLevelMode == LevelMode.Gold)
        {
            timerController.StartTimer(levelSettings.CountSecondsForGoldDificulty);
        }
    }

    private void SetGoals()
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

    public void ShowBox(int x, int y, MapCellType mapElement)
    {
        buttons[x, y].GetComponent<Image>().sprite = images[(int)mapElement].sprite;
    }

    public FigureType GetRandomFigureFromAvailable()
    {
        if (levelSettings.FiguresAvailableOnLevel == null)
        {
            throw new NullReferenceException();
        }

        int randomNumber = random.Next(0, levelSettings.FiguresAvailableOnLevel.Count);
        return levelSettings.FiguresAvailableOnLevel[randomNumber];
    }

    public void ShowStatistics(LevelProgress levelProgress)
    {
        int goalNumber = 1;
        ObjectManager.OutputInformation(MovesText, levelProgress.CountMoves.ToString());

        foreach (var keyValuePair in levelSettings.PieceToCollectAndCount)
        {
            string goalTextName = $"{GoalTextPrefix}{goalNumber++}";

            ObjectManager.OutputInformation(goalTextName, $"{GetFigureProgress(levelProgress, keyValuePair.Key)}/{keyValuePair.Value}");
        }

        if (CheckIsEndGame(levelProgress))
        {
            return;
        }

        countMoves = levelProgress.CountMoves;
        CheckIsItNeedToChangeCountStarsAndChangeIfNeeded(levelProgress);
    }

    private void CheckIsItNeedToChangeCountStarsAndChangeIfNeeded(LevelProgress levelProgress)
    {
        if (ApplicationData.CurrentLevelMode == LevelMode.Usual)
        {
            if (levelProgress.CountMoves == levelSettings.CountMoveFor3Stars)
            {
                countStars = 2;
                thirdStar.sprite = starOff.sprite;
                ObjectManager.OutputInformation(GoalMovesText, levelSettings.CountMoveFor2Stars.ToString());
            }

            if (levelProgress.CountMoves == levelSettings.CountMoveFor2Stars)
            {
                countStars = 1;
                secondStar.sprite = starOff.sprite;
                ObjectManager.OutputInformation(GoalMovesText, levelSettings.CountMoveFor1Star.ToString());
            }
        }
    }

    private bool CheckIsEndGame(LevelProgress levelProgress)
    {
        if (IsAllGoalsAchieved(levelProgress))
        {
            Win();
            return true;
        }

        if (levelProgress.CountMoves == levelSettings.CountMoveFor1Star)
        {
            Lose();
            return true;
        }

        return false;
    }

    private void Win()
    {
        ObjectManager.FindHiddenObjectAndSetActive(LvlCompletedObject);
        bool someChanges = false;

        void UpdateLevelStatus(LevelStatus newStatus)
        {
            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] < newStatus)
            {
                ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] = newStatus;
                someChanges = true;
            }
        }

        animationController.AnimateWin();

        if (ApplicationData.CurrentLevelMode == LevelMode.Usual)
        {
            SetStars();

            animationController.AnimateStars();
            UpdateLevelStatus((LevelStatus)countStars);
        }

        if (ApplicationData.CurrentLevelMode >= LevelMode.Silver)
        {
            timerController.StopTimer();
            HideStars();
            ObjectManager.FindHiddenObjectAndSetActive(WingsObject);
            UpdateLevelStatus(LevelStatus.SilverWings);
        }

        if (ApplicationData.CurrentLevelMode >= LevelMode.Gold)
        {
            timerController.StopTimer();
            ObjectManager.SetPicture(WingsObject, GoldenWingsImage);
            UpdateLevelStatus(LevelStatus.GoldenWings);
        }

        if (someChanges)
        {
            DataManipulator.SaveMapInformation(ApplicationData.MapInformation);
        }

        ApplicationData.ShopInformation = DataManipulator.LoadShopInformation();
        int rewardMoney = levelRewardSystem.CalculateReward(CalculateMaxMoves(), countMoves, timerController.GetRemainingTimeInSeconds());
        ApplicationData.ShopInformation.Money += rewardMoney;
        ObjectManager.OutputInformation(EarnedMoneyText, "+" + rewardMoney);
        DataManipulator.SaveShopInformation(ApplicationData.ShopInformation);
    }

    private int CalculateMaxMoves()
    {
        return ApplicationData.CurrentLevelMode switch
        {
            LevelMode.Usual => levelSettings.CountMoveFor1Star,
            LevelMode.Silver => 0,
            LevelMode.Gold => levelSettings.CountMovesForGoldDificulty,
            _ => 0,
        };
    }

    private void SetStars()
    {
        if (countStars >= 1)
        {
            ObjectManager.SetPicture($"{StarImagePrefix}1", StarOnImage);
        }

        if (countStars >= 2)
        {
            ObjectManager.SetPicture($"{StarImagePrefix}2", StarOnImage);
        }

        if (countStars == 3)
        {
            ObjectManager.SetPicture($"{StarImagePrefix}3", StarOnImage);
        }
    }

    private void HideStars()
    {
        ObjectManager.SetObjectNonActive($"{StarImagePrefix}1");
        ObjectManager.SetObjectNonActive($"{StarImagePrefix}2");
        ObjectManager.SetObjectNonActive($"{StarImagePrefix}3");
    }

    private void Lose()
    {
        if (ApplicationData.CurrentLevelMode == LevelMode.Usual)
        {
            firstStar.sprite = starOff.sprite;
        }

        if (ApplicationData.CurrentLevelMode >= LevelMode.Silver)
        {
            timerController.StopTimer();
        }

        ObjectManager.FindHiddenObjectAndSetActive(LvlFailedObject);
        animationController.AnimateLose();
    }

    private bool IsAllGoalsAchieved(LevelProgress levelProgress)
    {
        bool result = true;
        foreach (var keyValuePair in levelSettings.PieceToCollectAndCount)
        {
            if (!IsGoalAchieved(levelProgress, keyValuePair.Key, keyValuePair.Value))
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private bool IsGoalAchieved(LevelProgress levelProgress, FigureType figureType, int goalToCollect)
    {
        return figureType switch
        {
            FigureType.Pawn => levelProgress.CountCollectedPawns >= goalToCollect,
            FigureType.Knight => levelProgress.CountCollectedKnights >= goalToCollect,
            FigureType.Bishop => levelProgress.CountCollectedBishops >= goalToCollect,
            FigureType.Rook => levelProgress.CountCollectedRooks >= goalToCollect,
            FigureType.Queen => levelProgress.CountCollectedQueens >= goalToCollect,
            FigureType.King => levelProgress.CountCollectedKings >= goalToCollect,
            _ => false,
        };
    }

    private int GetFigureProgress(LevelProgress levelProgress, FigureType figureType)
    {
        return figureType switch
        {
            FigureType.Pawn => levelProgress.CountCollectedPawns,
            FigureType.Knight => levelProgress.CountCollectedKnights,
            FigureType.Bishop => levelProgress.CountCollectedBishops,
            FigureType.Rook => levelProgress.CountCollectedRooks,
            FigureType.Queen => levelProgress.CountCollectedQueens,
            FigureType.King => levelProgress.CountCollectedKings,
            _ => levelProgress.CountCollectedPawns,
        };
    }

    public void Click()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumber(name);
        int x = nr % MainMap.SIZE;
        int y = nr / MainMap.SIZE;
        board.Click(x, y);
    }

    private void InitButtons()
    {
        buttons = new Button[MainMap.SIZE, MainMap.SIZE];
        for (int nr = 0; nr < MainMap.SIZE * MainMap.SIZE; nr++)
        {
            buttons[nr % MainMap.SIZE, nr / MainMap.SIZE] = GameObject.Find($"Button ({nr})").GetComponent<Button>();
        }
    }

    private void InitImages()
    {
        images = new Image[MainMap.PIECES];
        for (int j = 0; j < MainMap.PIECES; j++)
        {
            images[j] = GameObject.Find($"Image ({j})").GetComponent<Image>();
        }

        starOff = GameObject.Find($"StarOff").GetComponent<Image>();
        firstStar = GameObject.Find($"Star1Statistics").GetComponent<Image>();
        secondStar = GameObject.Find($"Star2Statistics").GetComponent<Image>();
        thirdStar = GameObject.Find($"Star3Statistics").GetComponent<Image>();
    }

    private int GetNumber(string name)
    {
        Regex regex = new("\\((\\d+)\\)");
        Match match = regex.Match(name);
        if (!match.Success)
        {
            throw new Exception("Unrecognized object name");
        }

        Group group = match.Groups[1];
        string number = group.Value;

        return Convert.ToInt32(number);
    }
}