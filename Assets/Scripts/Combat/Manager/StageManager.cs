using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class StageManager : MonoBehaviour
    {
    #region Managers
        private EnemyManager _enemyManager;
        private EffectManager _effectManager;
        private WaveManager _waveManager;
    #endregion

        [SerializeField, Header("Temp")]
        private CharacterUnit characterPrefab;

        private CharacterUnit _characterUnit;
        private InputProcessor _inputProcessor;

        private void Awake()
        {
            _enemyManager = GetComponent<EnemyManager>();
            _effectManager = GetComponent<EffectManager>();
            _waveManager = new();
            _inputProcessor = new();
        }

        public void Initialize()
        {
            _characterUnit = Instantiate(characterPrefab);
            _characterUnit.SetDependency(this);
            _characterUnit.Initialize(0);
        }

        private void Update()
        {
            _inputProcessor.ProcessInput();
            
            _characterUnit.SetInput(_inputProcessor.Status);
            _characterUnit.Progress();
        }
    }
}
