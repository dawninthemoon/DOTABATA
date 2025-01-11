using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sector
{
    public class SectorNode : MonoBehaviour
    {
        private int _nodeKey;
        public int NodeKey => _nodeKey;

        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;

        public void Initialize(int nodeKey, int row, int column)
        {
            _nodeKey = nodeKey;
            _row = row;
            _column = column;
            gameObject.SetActive(true);
        }
    }
}