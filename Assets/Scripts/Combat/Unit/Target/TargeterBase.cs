using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public abstract class TargeterBase : MonoBehaviour
    {
        protected List<ITargetable> _targetList = new();
        public List<ITargetable> TargetList => _targetList;

        protected ITargetable _currentTarget;
        public ITargetable CurrentTarget => _currentTarget;

        protected virtual void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Targeter");
        }
    }
}
