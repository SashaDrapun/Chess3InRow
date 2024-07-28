using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataService
{
    [Serializable]
    public class MapInformation
    {
        public MapInformation() 
        {
            Levels = new List<LevelStatus>();
            for (int i = 0; i < 6; i++)
            {
                Levels.Add(LevelStatus.Failed);
            }
        }

        public List<LevelStatus> Levels;
    }
}
