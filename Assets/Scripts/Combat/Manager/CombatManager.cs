using System.Collections;
using System.Collections.Generic;
using Combat.Actions;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour
    {
    #region Managers
        private EnemyManager _enemyManager;
        public EnemyManager EnemyManager => _enemyManager;

        private CharacterManager _characterManager;
        public CharacterManager CharacterManager => _characterManager;

        private EffectManager _effectManager;
        private EffectManager EffectManager => _effectManager;

        private WaveManager _waveManager;
        private WaveManager WaveManager => _waveManager;

        private ActionManager _actionManager;
        public ActionManager ActionManager => _actionManager;

        private ProjectileManager _projectileManager;
        public ProjectileManager ProjectileManager => _projectileManager;
    #endregion

        private CharacterUnit _myCharacter;
        private InputProcessor _inputProcessor;

        private void Awake()
        {
            _enemyManager = GetComponentInChildren<EnemyManager>();
            _characterManager = GetComponentInChildren<CharacterManager>();
            _effectManager = GetComponentInChildren<EffectManager>();
            _projectileManager = GetComponentInChildren<ProjectileManager>();
            _waveManager = new();
            _actionManager = new(this);
            _inputProcessor = new();
        }

        public void Initialize()
        {
            _myCharacter = _characterManager.CreateCharacter(0, this);

            _enemyManager.CreateEnemy(0, this);
        }

        private void Update()
        {
            _inputProcessor.ProcessInput();
            
            _myCharacter.SetInput(_inputProcessor.Status);
            _myCharacter.Progress();
        }
    }
}
