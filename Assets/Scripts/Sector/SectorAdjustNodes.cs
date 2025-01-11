using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sector
{
    public class SectorAdjustNodes 
    {
        private bool[,] _adjustList;

        public void CreateList(int maxNodeSize)
        {
            _adjustList = new bool[maxNodeSize, maxNodeSize];
        }

        public void AdjustNode(int from, int to)
        {
            _adjustList[from, to] = true;
        }

        public bool IsAdjustNode(int from, int to)
        {
            return _adjustList[from, to];
        }
    }
}