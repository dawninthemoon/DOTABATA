using System.Collections;
using System.Collections.Generic;
using Sector;
using UnityEngine;

namespace Sector
{
    public class SectorManager : MonoBehaviour
    {
        [SerializeReference]
        private SectorGrid sectorGrid;
        [SerializeField]
        private SectorGenerator sectorGenerator;
        private SectorAdjustNodes _adjustNodes;

        private List<SectorNode> _sectorNodeList;

        private void Start()
        {
            _adjustNodes = new();
            _sectorNodeList = sectorGenerator.GenerateNodes(sectorGrid, _adjustNodes);
        }

        public void Regenerate()
        {
            _adjustNodes = new();

            foreach (var node in _sectorNodeList)
            {
                node.gameObject.SetActive(false);
                DestroyImmediate(node.gameObject);
            }
            _sectorNodeList = sectorGenerator.GenerateNodes(sectorGrid, _adjustNodes);
        }

        private void Update()
        {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                Regenerate();
            }
        #endif
        }

        private void OnDrawGizmos()
        {
            if (_sectorNodeList == null)
            {
                return;
            }

            Color defaultColor = Gizmos.color;

            foreach (var sector in _sectorNodeList)
            {
                int sectorKey = sector.NodeKey;
                foreach (var targetSector in _sectorNodeList)
                {
                    int targetSectorKey = targetSector.NodeKey;
                    if (sectorKey == targetSectorKey)
                    {
                        continue;
                    }

                    if (_adjustNodes.IsAdjustNode(sectorKey, targetSectorKey))
                    {
                        Gizmos.DrawLine(sector.transform.position, targetSector.transform.position);
                    }
                }
            }

            Gizmos.color = defaultColor;
        }
    }
}