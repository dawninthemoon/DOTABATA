using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyRenderer : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Move,
            Attack,
            Die,
        }

        private static readonly string[] SuffixArray = { "_Down", "_Right", "_Up", };

        [SerializeField]
        private SpriteAnimator animator;
        [SerializeField]
        private SpriteAnimator armAnimator;
        private State _currentState;
        private int _defaultAnimationIndex;

        public State CurrentState => _currentState;

        private void Start()
        {
            _currentState = State.Idle;
            _defaultAnimationIndex = -1;

            armAnimator.SetEndCallback("Attack", ChangeToIdle);
        }

        public void Reset()
        {
            _currentState = State.Idle;
            animator.ChangeAnimation(_currentState.ToString());
            armAnimator.ChangeAnimation(_currentState.ToString());
        }

        public void ChangeState(State state, bool forceReset)
        {   
            if (_currentState == state && !forceReset)
            {
                return;
            }

            _currentState = state;
            armAnimator.ChangeAnimation(state.ToString());
        }

        public void UpdateAnimator(ITargetable target)
        {
            Vector2 diff = Vector2.right;
            float angle = 0f;
            if (target != null)
            {
                diff = target.GetPosition() - (Vector2)transform.position;
                angle = Vector2.Angle(Vector2.down, diff);
            }
            
            ChangeArmDirection();
            void ChangeArmDirection()
            {
                armAnimator.Renderer.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }

            string animationName = _currentState.ToString();

            ChangeBodyDirection();
            void ChangeBodyDirection()
            {
                int animationIndex = Mathf.Clamp(Mathf.FloorToInt(angle / 180f * 3f), 0, SuffixArray.Length - 1);

                if (_defaultAnimationIndex != animationIndex)
                {
                    _defaultAnimationIndex = animationIndex;
                }

                if (_currentState == State.Idle || _currentState == State.Move)
                {
                    animationName += SuffixArray[_defaultAnimationIndex];
                }

                Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
                animator.transform.localScale = bodyScale;
            }

            animator.ChangeAnimation(animationName, resetIndex: (_currentState != State.Move));
        }

        private void ChangeToIdle()
        {
            ChangeState(State.Idle, true);
        }
    }
}