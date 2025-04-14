using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Sector
{
    public class SectorNode : MonoBehaviour
    {
        [SerializeField]
        private Button enterButton;

        private int _nodeKey;
        public int NodeKey => _nodeKey;

        private Rowcol _rowcol;
        public Rowcol Rowcol => _rowcol;
        public int Row => _rowcol.row;
        public int Column => _rowcol.column;

        private NodeType _type;
        public NodeType Type => _type;

        private void Awake()
        {
            enterButton.onClick.AddListener(OnClickEnterNode);
        }

        public void Initialize(int nodeKey, int row, int column)
        {
            _nodeKey = nodeKey;
            _rowcol = new Rowcol(row, column);

            gameObject.SetActive(true);
        }

        public void ChangeNodeType(NodeType type)
        {
            _type = type;
#if UNITY_EDITOR
            GetComponent<Image>().color = type switch
            {
                NodeType.Combat => Color.white,
                NodeType.Event => Color.cyan,
                NodeType.Shop => Color.magenta,
                _ => Color.white,
            };
#endif
        }

        private void OnClickEnterNode()
        {
            SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.CombatScene);
        }
    }

    public enum NodeType
    {
        Combat,
        Event,
        Shop,
    }
}