using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class SceneChanger : Singleton<SceneChanger>
    {
        public enum SceneType
        {
            SectorScene,
            CombatScene,
        }

        private SceneType _currentSceneType;

        public void ChangeScene(SceneType sceneType)
        {
            _currentSceneType = sceneType;
            SceneManager.LoadScene(_currentSceneType.ToString());
        }
    }
}
