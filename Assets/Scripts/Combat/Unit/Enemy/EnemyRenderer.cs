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

        private void Start()
        {
            _currentState = State.Idle;
            _defaultAnimationIndex = -1;

            armAnimator.SetEndCallback("Fire", ChangeToIdle);
        }

        public void ChangeState(State state)
        {   
            if (_currentState != state)
            {
                _currentState = state;
            }
        }

        public void UpdateAnimator(ITargetable target)
        {
            Vector2 diff = target.GetPosition() - (Vector2)transform.position;
            float angle = Vector2.Angle(Vector2.down, diff);

            ChangeArmDirection();
            void ChangeArmDirection()
            {
                armAnimator.Renderer.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }

            ChangeBodyDirection();
            void ChangeBodyDirection()
            {
                int animationIndex = Mathf.Clamp(Mathf.FloorToInt(angle / 180f * 3f), 0, SuffixArray.Length - 1);

                string animationName = _currentState.ToString();
                if (_defaultAnimationIndex != animationIndex)
                {
                    _defaultAnimationIndex = animationIndex;
                }

                if (_currentState == State.Idle || _currentState == State.Move)
                {
                    animationName += SuffixArray[_defaultAnimationIndex];
                }
                animator.ChangeAnimation(animationName, resetIndex: (_currentState != State.Move));

                Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
                animator.transform.localScale = bodyScale;
            }
        }

        private void ChangeToIdle()
        {
            armAnimator.ChangeAnimation("Idle");
        }
    }
}