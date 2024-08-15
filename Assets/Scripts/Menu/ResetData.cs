using Assets.Scripts.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ResetData : MonoBehaviour
    {
        public void Reset()
        {
            DataManipulator.ResetData();
        }
    }
}
