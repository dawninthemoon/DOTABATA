using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Sector
{
    public class SectorGrid : MonoBehaviour
    {
        [SerializeField]
        private int rowCount;
        [SerializeField]
        private int columnCount;

        public int RowCount => rowCount;
        public int ColumnCount => columnCount;

        private RectTransform _mapRect;

        private void Awake()
        {
            _mapRect = transform as RectTransform;
        }

        public Vector2 GetGridTopLeft(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 startPos = new Vector2(-size.x / 2f, size.y / 2f);
            Vector2 ceilSize = new Vector2(size.x / columnCount, size.y / rowCount);

            Vector2 pos = startPos;
            pos.x += ceilSize.x * column;
            pos.y -= ceilSize.y * row;

            return pos;
        }

        public Vector2 GetGridTopRight(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Debug.Log(size);
            Vector2 ceilSize = new Vector2(size.x / columnCount, size.y / rowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.x += ceilSize.x;

            return pos;
        }

        public Vector2 GetGridBottomLeft(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 ceilSize = new Vector2(size.x / columnCount, size.y / rowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.y -= ceilSize.y;

            return pos;
        }

        public Vector2 GetGridBottomRight(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 ceilSize = new Vector2(size.x / columnCount, size.y / rowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.x += ceilSize.x;
            pos.y -= ceilSize.y;

            return pos;
        }
    }
}