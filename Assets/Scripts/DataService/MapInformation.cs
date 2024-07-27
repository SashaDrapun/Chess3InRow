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
                Levels.Add(LevelStatus.failed);
            }
        }

        public List<LevelStatus> Levels;
    }
}
