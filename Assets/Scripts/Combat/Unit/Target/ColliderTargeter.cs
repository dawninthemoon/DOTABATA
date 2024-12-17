using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderTargeter : TargeterBase
    {
        [SerializeField]
        private TargetFaction detectFaction;

        private Collider2D _detectCollider;
        private float _detectRange;

        public delegate bool TargetValidCheckDelegate(TargetFaction faction, ITargetable target);
        private TargetValidCheckDelegate _validChecker;
        private System.Action<ITargetable> _onEnterTarget;
        private System.Action<ITargetable> _onExitTarget;

        protected override void Awake()
        {
            base.Awake();
            _detectCollider = GetComponent<Collider2D>();
            SetValidChecker(ValidChecker);
        }

        public void Reset()
        {
            _detectCollider.enabled = false;
            _detectCollider.enabled = true;
            _targetList.Clear();
            _currentTarget = null;
        }

        public void Initialize(float detectRange)
        {
            Reset();

            SetDetectRange(detectRange);
        }

        public void SetDetectRange(float detectRange)
        {
            _detectRange = detectRange;

            if (_detectCollider is CircleCollider2D)
            {
                var circleCollider = _detectCollider as CircleCollider2D;
                circleCollider.radius = detectRange;
            }
            else if (_detectCollider is BoxCollider2D)
            {
                // TODO
            }
        }

        public void SetUnitEnterEvent(System.Action<ITargetable> onEnterUnit)
        {
            _onEnterTarget = onEnterUnit;
        }

        public void SetUnitExitEvent(System.Action<ITargetable> onExitUnit)
        {
            _onExitTarget = onExitUnit;
        }

        public void ChangeCurrentTarget()
        {
            if (_targetList.Count == 0)
            {
                _currentTarget = null;
                return;
            }

            ITargetable found = null;
            float nearest = float.MaxValue;

            foreach (var target in _targetList)
            {
                float v = (target.GetPosition() - (Vector2)transform.position).sqrMagnitude;
                
                if (v < nearest)
                {
                    found = target;
                    nearest = v;
                }
            }

            if (found != null)
            {
                _currentTarget = found;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ITargetable target))
            {
                if (IsValidTarget(target))
                {
                    AddTarget(target);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ITargetable target))
            {
                RemoveTarget(target);
            }
        }

        private void AddTarget(ITargetable target)
        {
            if (target == null)
            {
                return;
            }

            target.OnDisappear += OnDisappearTarget;
            _targetList.Add(target);

            if (_currentTarget == null)
            {
                ChangeCurrentTarget();
            }

            OnAddTarget(target);
            _onEnterTarget?.Invoke(target);
        }

        protected virtual void OnAddTarget(ITargetable target)
        {

        }

        private void RemoveTarget(ITargetable target)
        {
            if (target == null)
            {
                return;
            }

            target.OnDisappear -= OnDisappearTarget;
            if (this == null)
            {
                return;
            }

            _targetList.Remove(target);

            if (_currentTarget == target)
            {
                ChangeCurrentTarget();
            }

            OnRemoveTarget(target);
            _onExitTarget?.Invoke(target);
        }

        protected virtual void OnRemoveTarget(ITargetable target)
        {

        }

        private void OnDisappearTarget(ITargetable target)
        {
            
        }

        public void SetValidChecker(TargetValidCheckDelegate checker)
        {
            _validChecker = checker;
        }

        protected virtual bool IsValidTarget(ITargetable target)
        {
            return _validChecker.Invoke(detectFaction, target);
        }

        public static bool ValidChecker(TargetFaction faction, ITargetable target)
        {
            if (target == null || faction != target.Faction)
            {
                return false;
            }
            return true;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                if (!collider.isTrigger)
                {
                    collider.isTrigger = true;
                    Debug.Log("IsTrigger Must be true");                   
                }
            }
        }
#endif
    }
}