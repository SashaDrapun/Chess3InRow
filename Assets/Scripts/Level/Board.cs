using Assets.Scripts.ChessFigures;
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

    public class Board
    {
        public Map map;
 

        public Board(ShowBox showBox)
        {
            map = new Map(showBox);
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