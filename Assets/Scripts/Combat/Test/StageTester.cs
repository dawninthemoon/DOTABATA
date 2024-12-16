using System.Collections;
using System.Collections.Generic;
using Game.StaticData;
using UnityEngine;

namespace Combat
{
    public class StageTester : MonoBehaviour
    {
        [SerializeField]
        private StageManager stageManager;

        private void Awake()
        {
            StaticDataManager.Instance.Initialize();
        }

        private void Start()
        {
            StartStage();
        }

        private void StartStage()
        {
            stageManager.Initialize();
        }
    }
}
