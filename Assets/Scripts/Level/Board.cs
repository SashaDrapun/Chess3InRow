using Assets.Scripts.ChessFigures;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public delegate void ShowBox(int x, int y, MapCellType mapElement);
    public delegate void ShowProgressOfTheLevel(LevelProgress levelProgress);

    public class Board
    {
        public Map map;
 
        public Board(ShowBox showBox,ShowProgressOfTheLevel showStatisticsOnTheScreen)
        {
            map = new Map(showBox, showStatisticsOnTheScreen);
        }

        public void Start()
        {
            map.Refresh();
        }

        public void Click(int x, int y)
        {
            map.Click(x, y);
        }
    }
}