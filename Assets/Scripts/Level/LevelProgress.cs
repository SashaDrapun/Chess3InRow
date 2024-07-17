using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Level
{
    public class LevelProgress
    {
        public LevelProgress() 
        { 
        }

        public int CountMoves { get; set; }

        public int CountCollectedPawns { get; set; }

        public int CountCollectedKnights { get; set; }

        public int CountCollectedBishops { get; set; }

        public int CountCollectedRooks { get; set; }

        public int CountCollectedQueens{ get; set; }

        public int CountCollectedKings { get; set; }

    }
}
