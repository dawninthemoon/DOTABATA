using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sector
{
    [System.Serializable]
    public class SectorArea
    {
        public int areaKey;
        public Rowcol starts;
        public Rowcol ends;

        public Rowcol GetRandomCoord()
        {
            int row = Random.Range(starts.row, ends.row);
            int column = Random.Range(starts.column, ends.column);

            Rowcol rc = new Rowcol()
            {
                row = row, 
                column = column
            };
            return rc;
        }
    }

    public class SectorGrid : MonoBehaviour
    {
        [SerializeField]
        private List<SectorArea> areaList;
        [SerializeField]
        private Rowcol gridSize;

        public List<SectorArea> AreaList => areaList;
        public int RowCount => gridSize.row;
        public int ColumnCount => gridSize.column;

        private RectTransform _mapRect;

        private void Awake()
        {
            _mapRect = transform as RectTransform;
        }

        public SectorArea GetArea(int areaKey)
        {
            return areaList.Find(x => x.areaKey == areaKey);
        }

        public Vector2 GetGridTopLeft(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 startPos = new Vector2(-size.x / 2f, size.y / 2f);
            Vector2 ceilSize = new Vector2(size.x / ColumnCount, size.y / RowCount);

            Vector2 pos = startPos;
            pos.x += ceilSize.x * column;
            pos.y -= ceilSize.y * row;

            return pos;
        }

        public Vector2 GetGridTopRight(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Debug.Log(size);
            Vector2 ceilSize = new Vector2(size.x / ColumnCount, size.y / RowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.x += ceilSize.x;

            return pos;
        }

        public Vector2 GetGridBottomLeft(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 ceilSize = new Vector2(size.x / ColumnCount, size.y / RowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.y -= ceilSize.y;

            return pos;
        }

        public Vector2 GetGridBottomRight(int row, int column)
        {
            Vector2 size = _mapRect.rect.size;
            Vector2 ceilSize = new Vector2(size.x / ColumnCount, size.y / RowCount);

            Vector2 pos = GetGridTopLeft(row, column);
            pos.x += ceilSize.x;
            pos.y -= ceilSize.y;

            return pos;
        }
    }
}