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

public class Game : MonoBehaviour
{

    Button[,] buttons;
    Image[] images;
    private Board board;


    void Start()
    {
        this.board = new Board(ShowBox, ShowStatistics);
        InitButtons();
        InitImages();
        board.Start();
        SetGoals();
    }

    private void SetGoals()
    {
        int goalNumber = 1;
        foreach (var key in ApplicationData.GoalsOnTheLevel.PieceToCollectAndCount.Keys)
        {
            string goalImageName = "GoalPicture" + goalNumber;
            string pictureName = GetPictureNameFromFigureType(key);
            string goalTextName = "GoalText" + goalNumber++;

            GameObject goalImageObject = FindHiddenObjectByName(goalImageName);
            Image goalImage = goalImageObject.GetComponent<Image>();
            Image goalPicture = GameObject.Find(pictureName).GetComponent<Image>();
            goalImageObject.SetActive(true);
            goalImage.sprite = goalPicture.sprite;

            GameObject goalTextObject = FindHiddenObjectByName(goalTextName);
            goalTextObject.SetActive(true);
            OutputInformation(goalTextName, $"0/{ApplicationData.GoalsOnTheLevel.PieceToCollectAndCount[key]}");
        }
        OutputInformation("Moves", "0");
        OutputInformation("GoalMoves", ApplicationData.GoalsOnTheLevel.CountMoveFor3Stars.ToString());
    }

    private GameObject FindHiddenObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }

    private string GetPictureNameFromFigureType(FigureType figureType)
    {
        switch (figureType)
        {
            case FigureType.Pawn:
                return "Pawn";
            case FigureType.Knight:
                return "Knight";
            case FigureType.Bishop:
                return "Bishop";
            case FigureType.Rook:
                return "Rook";
            case FigureType.Queen:
                return "Queen";
            case FigureType.King:
                return "King";
            default: return "Pawn";
        }
    }

    public void ShowBox(int x, int y, MapCellType mapElement)
    {
        buttons[x, y].GetComponent<Image>().sprite = images[(int)mapElement].sprite;
    }

    public void ShowStatistics(LevelProgress levelProgress)
    {
        int goalNumber = 1;
        OutputInformation("Moves", levelProgress.CountMoves.ToString());
        foreach (var key in ApplicationData.GoalsOnTheLevel.PieceToCollectAndCount.Keys)
        {
            string goalTextName = "GoalText" + goalNumber++;

            GameObject goalTextObject = FindHiddenObjectByName(goalTextName);
            OutputInformation(goalTextName, $"{GetFigureProgress(levelProgress, key)}/{ApplicationData.GoalsOnTheLevel.PieceToCollectAndCount[key]}");
        }
    }
    
    private void CheckIsItNeedToChangeCountStars(LevelProgress levelProgress)
    {
        
    }

    private void CheckIsEndGame()
    {

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

    private void OutputInformation(string textMeshProName, string outputInformation)
    {
        TextMeshProUGUI[] textMeshProObjects = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textMeshProObjects)
        {
            if (obj.name == textMeshProName)
            {
                obj.text = outputInformation;
                return;
            }
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

    }

    private int GetNumber(string name)
    {
        Regex regex = new("\\((\\d+)\\)");
        Match match = regex.Match(name);
        if (!match.Success)
        {
            throw new System.Exception("Unrecognized object name");
        }

        Group group = match.Groups[1];
        string number = group.Value;

        return Convert.ToInt32(number);
    }
}
