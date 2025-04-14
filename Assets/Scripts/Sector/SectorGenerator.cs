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

        private static readonly float[] ProbByTypes = new float[] { 0.35f, 0.55f, 0.1f, };
        private static readonly int[] MinNodeCountByTypes = new int[] { 1, 3, 1, };
        
        private void Awake()
        {
            nodePrefab.gameObject.SetActive(false);
        }
        
        public List<SectorNode> GenerateNodes(SectorGrid sectorGrid, SectorAdjustNodes adjustNodes)
        {
            int totalNodes = Random.Range(10, 15);
            List<SectorNode> nodeList = new List<SectorNode>();
            bool[,] nodeGrid = new bool[sectorGrid.RowCount, sectorGrid.ColumnCount];
            adjustNodes.CreateList(sectorGrid.RowCount * sectorGrid.ColumnCount);

            Dictionary<int, List<Rowcol>> availableCoords = new Dictionary<int, List<Rowcol>>();
            foreach (var area in sectorGrid.AreaList)
            {
                List<Rowcol> coords = new List<Rowcol>();
                for (int r = area.starts.row; r <= area.ends.row; r++)
                {
                    for (int c = area.starts.column; c <= area.ends.column; c++)
                    {
                        coords.Add(new Rowcol(r, c));
                    }
                }
                availableCoords.Add(area.areaKey, coords);
            }

            int startAreaKey = Random.Range(0, 2);
            Rowcol startCoord = availableCoords[startAreaKey].SelectOne();
            SectorNode startNode = CreateNodeByType(NodeType.Combat, startCoord, sectorGrid, nodeGrid);
            startNode.GetComponent<Image>().color = Color.yellow;
            nodeList.Add(startNode);

            int endAreaKey = Random.Range(4, 6);
            Rowcol endCoord = availableCoords[endAreaKey].SelectOne();
            SectorNode endNode = CreateNodeByType(NodeType.Combat, endCoord, sectorGrid, nodeGrid);
            endNode.GetComponent<Image>().color = Color.red;
            nodeList.Add(endNode);

            Rowcol current = startCoord;
            List<Rowcol> backbone = new List<Rowcol>();
            while (!(current.row == endCoord.row && current.column == endCoord.column))
            {
                List<Rowcol> candidates = new List<Rowcol>();
                int dr = endCoord.row - current.row;
                int dc = endCoord.column - current.column;
                int stepRow = (dr > 0) ? 1 : (dr < 0) ? -1 : 0;
                int stepCol = (dc > 0) ? 1 : (dc < 0) ? -1 : 0;

                if (stepRow != 0 && stepCol != 0)
                {
                    candidates.Add(new Rowcol(current.row + stepRow, current.column));
                    candidates.Add(new Rowcol(current.row, current.column + stepCol));
                    candidates.Add(new Rowcol(current.row + stepRow, current.column + stepCol));
                }
                else if (stepRow != 0)
                {
                    candidates.Add(new Rowcol(current.row + stepRow, current.column));
                }
                else if (stepCol != 0)
                {
                    candidates.Add(new Rowcol(current.row, current.column + stepCol));
                }

                candidates = candidates.Where(c => c.row >= 0 && c.row < sectorGrid.RowCount && c.column >= 0 && c.column < sectorGrid.ColumnCount).ToList();
                
                if (candidates.Count == 0)
                {
                    break;
                }

                Rowcol nextStep = candidates[Random.Range(0, candidates.Count)];
                backbone.Add(nextStep);
                current = nextStep;
            }

            foreach (var coord in backbone)
            {
                if (!nodeGrid[coord.row, coord.column])
                {
                    SectorNode node = CreateNode(coord, sectorGrid, nodeGrid);
                    nodeList.Add(node);
                }
            }

            while (nodeList.Count < totalNodes)
            {
                List<Rowcol> frontier = new List<Rowcol>();
                for (int r = 0; r < sectorGrid.RowCount; r++)
                {
                    for (int c = 0; c < sectorGrid.ColumnCount; c++)
                    {
                        if (!nodeGrid[r, c])
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                int nr = r + DirectonRow[i];
                                int nc = c + DirectonColumn[i];
                                
                                if (nr >= 0 && nr < sectorGrid.RowCount && nc >= 0 && nc < sectorGrid.ColumnCount && nodeGrid[nr, nc])
                                {
                                    frontier.Add(new Rowcol(r, c));
                                    break;
                                }
                            }
                        }
                    }
                }

                if (frontier.Count == 0)
                {
                    break;
                }

                Rowcol selected = frontier[Random.Range(0, frontier.Count)];
                SectorNode newNode = CreateNode(selected, sectorGrid, nodeGrid);
                nodeList.Add(newNode);
            }

            for (int r = 0; r < sectorGrid.RowCount; r++)
            {
                for (int c = 0; c < sectorGrid.ColumnCount; c++)
                {
                    if (nodeGrid[r, c])
                    {
                        int nodeKey = r * sectorGrid.ColumnCount + c;

                        for (int d = 0; d < 8; d++)
                        {
                            int nr = r + DirectonRow[d];
                            int nc = c + DirectonColumn[d];

                            if (IsValidNode(nr, nc, sectorGrid.RowCount, sectorGrid.ColumnCount, nodeGrid))
                            {
                                int targetKey = nr * sectorGrid.ColumnCount + nc;
                                adjustNodes.AdjustNode(nodeKey, targetKey);
                            }
                        }
                    }
                }
            }

            ApplyNodeTypes();
            void ApplyNodeTypes()
            {
                int usedCount = 0;
                int currentNodeType = 0;

                var shuffledNodeList = nodeList.Shuffle();
                for (int i = 0; i < shuffledNodeList.Count; ++i)
                {
                    var node = shuffledNodeList[i];
                    if (node.Rowcol == startCoord || node.Rowcol == endCoord)
                    {
                        continue;
                    }

                    NodeType selectedType = NodeType.Combat;
                    if (currentNodeType < MinNodeCountByTypes.Length)
                    {
                        if (usedCount < MinNodeCountByTypes[currentNodeType])
                        {
                            selectedType = (NodeType)currentNodeType;
                        }
                        else
                        {
                            ++currentNodeType;
                            usedCount = 0;
                            --i;
                            continue;
                        }
                        ++usedCount;
                    }
                    else
                    {
                        selectedType = SelectType();
                    }

                    node.ChangeNodeType(selectedType);
                }
            }

            return nodeList;
        }

        private SectorNode CreateNodeByType(NodeType type, Rowcol rowcol, SectorGrid sectorGrid, bool[,] nodeGrid)
        {
            SectorNode newNode = CreateNode(rowcol.row, rowcol.column, sectorGrid, nodeGrid);
            newNode.ChangeNodeType(type);
            return newNode;
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

        private NodeType SelectType()
        {
            float randValue = Random.value;

            float sum = 0f;
            for (int type = 0; type < ProbByTypes.Length; ++type)
            {
                sum += ProbByTypes[type];
                if (randValue <= sum)
                {
                    return (NodeType)type;
                }
            }

            return NodeType.Shop;
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
