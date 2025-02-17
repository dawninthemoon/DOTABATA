using System.Collections;
using System.Collections.Generic;
using Combat.Actions;
using UnityEngine;
using Game.StaticData;

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
        public EffectManager EffectManager => _effectManager;

        private StageManager _stageManager;
        public StageManager WaveManager => _stageManager;

        private ActionManager _actionManager;
        public ActionManager ActionManager => _actionManager;

        private ProjectileManager _projectileManager;
        public ProjectileManager ProjectileManager => _projectileManager;

        private VehicleManager _vehicleManager;
        public VehicleManager VehicleManager => _vehicleManager;
    #endregion

        private CharacterUnit _myCharacter;
        private InputProcessor _inputProcessor;

        private void Awake()
        {
            _enemyManager = GetComponentInChildren<EnemyManager>();
            _characterManager = GetComponentInChildren<CharacterManager>();
            _effectManager = GetComponentInChildren<EffectManager>();
            _projectileManager = GetComponentInChildren<ProjectileManager>();
            _stageManager = GetComponentInChildren<StageManager>();
            _vehicleManager = GetComponentInChildren<VehicleManager>();
            
            _actionManager = new(this);
            _inputProcessor = new();

            _stageManager.SetDependency(this);
            _vehicleManager.SetDependency(this);
            _projectileManager.SetDependency(this);
            _enemyManager.SetDependency(this);
        }

        public void Initialize()
        {
            _stageManager.Initialize();

            var stageData = StaticDataManager.Instance.GetStageByKey(0);
            _enemyManager.Initialize(stageData.monsters);

            _vehicleManager.Initialize();

            _myCharacter = _characterManager.CreateCharacter(0, this);
            _stageManager.StartStage(0);
        }

        private void Update()
        {
            _stageManager.ProcessWave();

            _inputProcessor.ProcessInput();
            
            _myCharacter.SetInput(_inputProcessor.Status);
            _myCharacter.Progress();
        }
    }
}
