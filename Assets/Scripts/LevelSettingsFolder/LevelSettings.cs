using Assets.Scripts.ChessFigures;
using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelSettingsFolder
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        public string levelName;
        public int levelNumber;
        public List<FigureType> FiguresAvailableOnLevel;
        public int CountMoveFor3Stars;
        public int CountMoveFor2Stars;
        public int CountMoveFor1Star;
        public SerializableDictionary<FigureType, int> PieceToCollectAndCountSerializable;
        public int CountSecondsForSilverDificulty;
        public int CountSecondsForGoldDificulty;
        public int CountMovesForGoldDificulty;

        public Dictionary<FigureType, int> PieceToCollectAndCount 
        { 
            get => PieceToCollectAndCountSerializable.ToDictionary(); 
            set => PieceToCollectAndCountSerializable.FromDictionary(value); 
        }

    }
}
