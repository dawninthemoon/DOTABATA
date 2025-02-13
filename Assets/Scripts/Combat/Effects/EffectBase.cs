using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EffectBase : MonoBehaviour
    {
        [SerializeField]
        public bool autoRelease;

        protected CombatManager _combatManager;
        protected System.Action _onDestroy;
        protected bool _initialized;

        public virtual void Init(System.Action onDestroy = null)
        {
            _onDestroy = onDestroy;
            _initialized = true;
        }

        public void SetDependency(CombatManager combatManager)
        {
            _combatManager = combatManager;
        }

        private void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        public void Release()
        {
            gameObject.SetActive(false);
        }
    }
}
