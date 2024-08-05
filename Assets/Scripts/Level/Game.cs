using Assets.Scripts;
using Assets.Scripts.ChessFigures;
using Assets.Scripts.Level;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using System.Diagnostics;
using Assets.Scripts.DataService;
using Assets.Scripts.LevelSettingsFolder;
using Assets.Scripts.GeneralFunctionality;

public class Game : MonoBehaviour
{
    public LevelManager levelManager;
    public TimerController timerController;

    private LevelSettings levelSettings;
    private static System.Random random = new();

    Button[,] buttons;
    Image[] images;
    private Board board;
    Image starOff;
    Image firstStar;
    Image secondStar;
    Image thirdStar;

    int countStars = 3;

    void Start()
    {
        LoadLevelSettings();
        InitButtons();
        InitImages();
        SetGoals();
        
        this.board = new Board(ShowBox, ShowStatistics, GetRandomFigureFromAvailable);
        board.Start();

        SetElements();
    }

    private void LoadLevelSettings()
    {
        levelSettings = levelManager.GetLevelSettings(ApplicationData.CurrentLevel);
    }

    private void SetElements()
    {
        if (ApplicationData.CurrentLevelMode >= LevelMode.Silver)
        {
            ObjectManager.SetObjectNonActive("Stars");
            ObjectManager.SetObjectNonActive("InfinitySymbol");
            ObjectManager.FindHiddenObjectAndSetActive("Timer");
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
            string goalImageName = "GoalPicture" + goalNumber;
            string pictureName = keyValuePair.Key.ToString();
            string goalTextName = "GoalText" + goalNumber++;

            ObjectManager.FindHiddenObjectAndSetActive(goalImageName);
            ObjectManager.FindHiddenObjectAndSetActive(goalTextName);
            ObjectManager.SetPicture(goalImageName, pictureName);
                       
            
            ObjectManager.OutputInformation(goalTextName, $"0/{levelSettings.PieceToCollectAndCount[keyValuePair.Key]}");
        }

        ObjectManager.OutputInformation("Moves", "0");
        ObjectManager.OutputInformation("GoalMoves", levelSettings.CountMoveFor3Stars.ToString());
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
        ObjectManager.OutputInformation("Moves", levelProgress.CountMoves.ToString());

        foreach (var keyValuePair in levelSettings.PieceToCollectAndCount)
        {
            string goalTextName = "GoalText" + goalNumber++;

            ObjectManager.OutputInformation(goalTextName, $"{GetFigureProgress(levelProgress, keyValuePair.Key)}/{keyValuePair.Value}");
        }

        if (CheckIsEndGame(levelProgress))
        {
            return;
        }
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
                ObjectManager.OutputInformation("GoalMoves", levelSettings.CountMoveFor2Stars.ToString());
            }

            if (levelProgress.CountMoves == levelSettings.CountMoveFor2Stars)
            {
                countStars = 1;
                secondStar.sprite = starOff.sprite;
                ObjectManager.OutputInformation("GoalMoves", levelSettings.CountMoveFor1Star.ToString());
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
        ObjectManager.FindHiddenObjectAndSetActive("LVLCompleted");
        bool someChanges = false;
        if (ApplicationData.CurrentLevelMode == LevelMode.Usual)
        {
            if ((int)ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] < countStars)
            {
                ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] = (LevelStatus)countStars;
            }

            someChanges = true;
        }


        if (ApplicationData.CurrentLevelMode >= LevelMode.Silver)
        {
            timerController.StopTimer();

            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] < LevelStatus.SilverWings)
            {
                ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] = LevelStatus.SilverWings;
                someChanges = true;
            }
        }
        
        if (someChanges)
        {
            DataManipulator dataManipulator = new DataManipulator();
            dataManipulator.SaveMapInformation(ApplicationData.MapInformation);
        }
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

        
        
        ObjectManager.FindHiddenObjectAndSetActive("LVLFailed");
    }

    

    private bool IsAllGoalsAchieved(LevelProgress levelProgress)
    {
        bool result = true;
        foreach (var keyValuePair in levelSettings.PieceToCollectAndCount)
        {
            result = IsGoalAchieved(levelProgress, keyValuePair.Key, keyValuePair.Value);
        }

        return result;
    }

    private bool IsGoalAchieved(LevelProgress levelProgress, FigureType figureType, int goalToCollect)
    {
        switch (figureType)
        {
            case FigureType.Pawn:
                return levelProgress.CountCollectedPawns >= goalToCollect;
            case FigureType.Knight:
                return levelProgress.CountCollectedKnights >= goalToCollect;
            case FigureType.Bishop:
                return levelProgress.CountCollectedBishops >= goalToCollect;
            case FigureType.Rook:
                return levelProgress.CountCollectedRooks >= goalToCollect;
            case FigureType.Queen:
                return levelProgress.CountCollectedQueens >= goalToCollect;
            case FigureType.King:
                return levelProgress.CountCollectedKings >= goalToCollect;
            default: return false;
        }
    }

    private int GetFigureProgress(LevelProgress levelProgress, FigureType figureType)
    {
        switch (figureType)
        {
            case FigureType.Pawn:
                return levelProgress.CountCollectedPawns;
            case FigureType.Knight:
                return levelProgress.CountCollectedKnights;
            case FigureType.Bishop:
                return levelProgress.CountCollectedBishops;
            case FigureType.Rook:
                return levelProgress.CountCollectedRooks;
            case FigureType.Queen:
                return levelProgress.CountCollectedQueens;
            case FigureType.King:
                return levelProgress.CountCollectedKings;
            default: return levelProgress.CountCollectedPawns;
        }
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
