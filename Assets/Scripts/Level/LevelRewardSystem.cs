using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelRewardSystem
    {
        public LevelRewardSystem()
        {
        }

        public int CalculateReward(int maxMoves, int actualMoves, int remainingTimeInSeconds)
        {
            return ApplicationData.CurrentLevelMode switch
            {
                LevelMode.Usual => 5 + (maxMoves - actualMoves) * 10,
                LevelMode.Silver => 10 + remainingTimeInSeconds,
                LevelMode.Gold => 20 + (maxMoves - actualMoves) * 10 + remainingTimeInSeconds,
                _ => 0,
            };
        }
    }
}
