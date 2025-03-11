using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Utils;

namespace Sector
{
    public class SectorGenerator : MonoBehaviour
    {
        [SerializeField]
        private SectorNode nodePrefab;
        [SerializeField]
        private RectTransform nodeParent;

        private static readonly int[] DirectonRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private static readonly int[] DirectonColumn = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        private void Awake()
        {
            nodePrefab.gameObject.SetActive(false);
        }

        public List<SectorNode> GenerateNodes(SectorGrid sectorGrid, SectorAdjustNodes adjustNodes)
        {
            Dictionary<int, List<Rowcol>> availableCoords = new();
            List<SectorNode> nodeList = new();
            bool[, ] nodeGrid = new bool[sectorGrid.RowCount, sectorGrid.ColumnCount];
            adjustNodes.CreateList(sectorGrid.RowCount * sectorGrid.ColumnCount);

            foreach (var area in sectorGrid.AreaList)
            {
                for (int r = area.starts.row; r <= area.ends.row; ++r)
                {
                    for (int c = area.starts.column; c <= area.ends.column; ++c)
                    {
                        if (!availableCoords.ContainsKey(area.areaKey))
                        {
                            availableCoords.Add(area.areaKey, new());
                        }
                        availableCoords[area.areaKey].Add(new Rowcol(r, c));
                    }
                }
            }

            int startAreaKey = Random.Range(0, 2);
            int endAreaKey = Random.Range(4, 5);

            var startNode = GenerateStartNode(startAreaKey);
            GenerateNodeByArea(1 - startAreaKey, 1);
            GenerateNodeByArea(2, 2);
            GenerateNodeByArea(3, 2);
            GenerateNodeByArea(4, 2);
            GenerateNodeByArea(5, 2);

            SectorNode GenerateStartNode(int startAreaKey)
            {
                var startCoord = availableCoords[startAreaKey].SelectOne();
                
                var startNode = CreateNode(startCoord, sectorGrid, nodeGrid);
                startNode.GetComponent<Image>().color = Color.yellow;
                nodeList.Add(startNode);

                return startNode;
            }

            void GenerateNodeByArea(int areaKey, int maxSize)
            {
                int size = maxSize;
                for (int i = 0; i < size; ++i)
                {
                    var coord = availableCoords[areaKey].SelectOne();
                
                    var startNode = CreateNode(coord, sectorGrid, nodeGrid);
                    nodeList.Add(startNode);
                }
            }

/*
            for (int i = 0; i < DirectonRow.Length; ++i)
            {
                int row = startNode.Row + DirectonRow[i];
                int col = startNode.Column + DirectonColumn[i];
                if (IsValidNode(row, col, sectorGrid.RowCount, sectorGrid.ColumnCount, nodeGrid))
                {
                    var newNode = CreateNode(row, col, sectorGrid, nodeGrid);
                    nodeList.Add(newNode);
                }
            }

            for (int row = 0; row < sectorGrid.RowCount; ++row)
            {
                for (int col = 0; col < sectorGrid.ColumnCount; ++col)
                {
                    if (!nodeGrid[row, col] && Random.value < 0.7f)
                    {
                        var newNode = CreateNode(row, col, sectorGrid, nodeGrid);
                        nodeList.Add(newNode);
                    }
                }
            }

            for (int i = 0; i < nodeList.Count; ++i)
            {
                var target = nodeList[i];
                if (target.NodeKey == startNode.NodeKey)
                {
                    continue;
                }
                
                Rowcol dest = new Rowcol() { row = target.Row, column = target.Column };
                if (!IsValidDestination(startNode.Rowcol, dest, sectorGrid.RowCount, sectorGrid.ColumnCount, nodeGrid))
                {
                    nodeGrid[dest.row, dest.column] = false;

                    nodeList.RemoveAt(i--);
                    target.gameObject.SetActive(false);
                    DestroyImmediate(target.gameObject);
                    continue;
                }
            }

            for (int row = 0; row < sectorGrid.RowCount; ++row)
            {
                int vertexCount = 0;
                for (int col = 0; col < sectorGrid.ColumnCount; ++col)
                {
                    if (nodeGrid[row, col])
                    {
                        AdjustNodes(row, col);
                    }
                }
            }

            nodeList[nodeList.Count - 1].GetComponent<Image>().color = Color.red;
*/

            return nodeList;

            void AdjustNodes(int originRow, int originCol)
            {
                int nodeKey = originRow * sectorGrid.ColumnCount + originCol;

                for (int i = 0; i < DirectonRow.Length; ++i)
                {
                    int newRow = originRow + DirectonRow[i];
                    int newCol = originCol + DirectonColumn[i];

                    if (IsValidNode(newRow, newCol, sectorGrid.RowCount, sectorGrid.ColumnCount, nodeGrid))
                    {
                        int targetKey = newRow * sectorGrid.ColumnCount + newCol;
                        adjustNodes.AdjustNode(nodeKey, targetKey);
                    }
                }
            }
        }

        private SectorNode CreateNode(Rowcol rowcol, SectorGrid sectorGrid, bool[,] nodeGrid)
        {
            return CreateNode(rowcol.row, rowcol.column, sectorGrid, nodeGrid);
        }

        private SectorNode CreateNode(int row, int column, SectorGrid sectorGrid, bool[,] nodeGrid)
        {
            SectorNode newNode = Instantiate(nodePrefab, nodeParent);
            newNode.Initialize(row * sectorGrid.ColumnCount + column, row, column);

            Vector2 topLeft = sectorGrid.GetGridTopLeft(row, column);
            Vector2 bottomRight = sectorGrid.GetGridBottomRight(row, column);

            topLeft += new Vector2(10f, -10f);
            bottomRight += new Vector2(-10f, 10f);

            float randX = Random.Range(topLeft.x, bottomRight.x);
            float randY = Random.Range(topLeft.y, bottomRight.y);

            newNode.transform.localPosition = new Vector3(randX, randY);

            nodeGrid[row, column] = true;
            return newNode;
        }

        private bool IsValidDestination(Rowcol start, Rowcol destination, int maxRow, int maxCol, bool[, ] nodes)
        {
            bool isValid = false;

            bool[, ] visit = new bool[maxRow, maxCol];
            Stack<Rowcol> bfsStack = new();

            bfsStack.Push(new Rowcol() { row = start.row, column = start.column });
            visit[start.row, start.column] = true;

            while (bfsStack.Count > 0)
            {
                Rowcol rowcol = bfsStack.Pop();

                for (int i = 0; i < DirectonRow.Length; ++i)
                {
                    int newRow = rowcol.row + DirectonRow[i];
                    int newCol = rowcol.column + DirectonColumn[i];
                    if (IsValidNode(newRow, newCol, maxRow, maxCol, nodes))
                    {
                        if (newRow == destination.row && newCol == destination.column)
                        {
                            return true;
                        }

                        if (visit[newRow, newCol])
                        {
                            continue;
                        }

                        visit[newRow, newCol] = true;
                        Rowcol destRowcol = new Rowcol() { row = newRow, column = newCol };
                        bfsStack.Push(destRowcol);
                    }
                }
            }

            return isValid;
        }

        private bool IsValidNode(int row, int column, int maxRow, int maxColumn, bool[, ] nodeGrid)
        {
            return 
                    (row >= 0)
                    && (row < maxRow)
                    && (column >= 0)
                    && (column < maxColumn)
                    && nodeGrid[row, column];
        } 
    }
}
