using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public List<SectorNode> GenerateNodes(SectorGrid sectorGrid, SectorAdjustNodes adjustNodes)
        {
            List<SectorNode> nodeList = new();
            bool[, ] nodeGrid = new bool[sectorGrid.RowCount, sectorGrid.ColumnCount];
            adjustNodes.CreateList(sectorGrid.RowCount * sectorGrid.ColumnCount);

            int startNodeKey = -1;
            Rowcol startRowcol = new Rowcol();

            for (int row = 0; row < sectorGrid.RowCount; ++row)
            {
                for (int col = 0; col < sectorGrid.ColumnCount; ++col)
                {
                    if (Random.value < 0.7f)
                    {
                        SectorNode newNode = Instantiate(nodePrefab, nodeParent);
                        newNode.Initialize(row * sectorGrid.ColumnCount + col, row, col);

                        if (startNodeKey == -1)
                        {
                            startNodeKey = row * sectorGrid.ColumnCount + col;
                            startRowcol = new Rowcol() { row = row, column = col };
                        }

                        Vector2 topLeft = sectorGrid.GetGridTopLeft(row, col);
                        Vector2 bottomRight = sectorGrid.GetGridBottomRight(row, col);

                        topLeft += new Vector2(10f, -10f);
                        bottomRight += new Vector2(-10f, 10f);

                        float randX = Random.Range(topLeft.x, bottomRight.x);
                        float randY = Random.Range(topLeft.y, bottomRight.y);

                        newNode.transform.localPosition = new Vector3(randX, randY);
                        nodeList.Add(newNode);

                        nodeGrid[row, col] = true;
                    }
                }
            }

            for (int i = 0; i < nodeList.Count; ++i)
            {
                var target = nodeList[i];
                if (target.NodeKey == startNodeKey)
                {
                    continue;
                }
                
                Rowcol dest = new Rowcol() { row = target.Row, column = target.Column };
                if (!IsValidDestination(startRowcol, dest, sectorGrid.RowCount, sectorGrid.ColumnCount, nodeGrid))
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
