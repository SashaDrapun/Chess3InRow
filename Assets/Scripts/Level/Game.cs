using Assets.Scripts;
using Assets.Scripts.ChessFigures;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Button[,] buttons;
    Image[] images;
    private Board board;

    void Start()
    {
        this.board = new Board(ShowBox);
        InitButtons();
        InitImages();
        board.Start();
    }

    public void ShowBox(int x, int y, MapCellType mapElement)
    {
        buttons[x, y].GetComponent<Image>().sprite = images[(int)mapElement].sprite;
    }

    public void Click()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumber(name);
        int x = nr % Map.SIZE;
        int y = nr / Map.SIZE;
        board.Click(x, y);
    }

    private void InitButtons()
    {
        buttons = new Button[Map.SIZE, Map.SIZE];
        for (int nr = 0; nr < Map.SIZE * Map.SIZE; nr++)
        {
            buttons[nr % Map.SIZE, nr / Map.SIZE] = GameObject.Find($"Button ({nr})").GetComponent<Button>();
        }
    }

    private void InitImages()
    {
        images = new Image[Map.PIECES];
        for (int j = 0; j < Map.PIECES; j++)
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
