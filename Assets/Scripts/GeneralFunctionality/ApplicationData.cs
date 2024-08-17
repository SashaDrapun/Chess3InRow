using Assets.Scripts.ChessFigures;
using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using Assets.Scripts.Level;
using Assets.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationData
{
    public static int CurrentLevel;

    public static LevelMode CurrentLevelMode;

    public static MapInformation MapInformation;

    public static ShopInformation ShopInformation;

    public static FromWhereGoToShop FromWhereGoToShop;
}
