using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class TargeterBase : MonoBehaviour
    {
        private List<ITargetable> _targetList;
        protected ITargetable _currentTarget;
        public ITargetable CurrentTarget => _currentTarget;
    }
}
