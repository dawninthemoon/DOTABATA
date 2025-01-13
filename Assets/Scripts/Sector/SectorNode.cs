using System.Collections;
using System.Collections.Generic;
using Game.Core;
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

        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;

        private void Awake()
        {
            enterButton.onClick.AddListener(OnClickEnterNode);
        }

        public void Initialize(int nodeKey, int row, int column)
        {
            _nodeKey = nodeKey;
            _row = row;
            _column = column;
            gameObject.SetActive(true);
        }

        private void OnClickEnterNode()
        {
            SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.CombatScene);
        }
    }
}