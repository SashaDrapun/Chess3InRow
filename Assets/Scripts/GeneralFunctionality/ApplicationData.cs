using Assets.Scripts.ChessFigures;
using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationData
{
    public static string SelectedLevel;

    public static int CurrentLevel;

    public static List<FigureType> FiguresAvailableOnLevel;

    public static GoalsOnTheLevel GoalsOnTheLevel;

    public static MapInformation MapInformation;
}
