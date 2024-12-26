using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CharacterRenderer : MonoBehaviour
    {
        public enum BodyState
        {
            Idle,
            Move,
            Freeze,
            Down,
        }

        public enum ArmState
        {
            Idle,
            Fire,
            Reload,
        }

        private static readonly string[] SuffixArray = { "_Down", "_Right", "_Up", };

        [SerializeField]
        private SpriteAnimator bodyAnimator;
        [SerializeField]
        private SpriteAnimator armAnimator;
        [field: SerializeField]
        public Transform BulletTransform { get; private set; }
        public BodyState CurrentState { get; private set; }
        private int _defaultAnimationIndex;

        private void Start()
        {
            CurrentState = BodyState.Idle;
            _defaultAnimationIndex = -1;

            armAnimator.SetEndCallback("Fire", ChangeToIdle);
        }

        private void Update()
        {
            Vector2 mousePosition = Game.Utils.ExMouse.GetMouseWorldPosition();

            Vector2 diff = mousePosition - (Vector2)transform.position;
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

                string animationName = CurrentState.ToString();
                if (_defaultAnimationIndex != animationIndex)
                {
                    _defaultAnimationIndex = animationIndex;
                }

                if (CurrentState == BodyState.Idle || CurrentState == BodyState.Move)
                {
                    animationName += SuffixArray[_defaultAnimationIndex];
                }
                bodyAnimator.ChangeAnimation(animationName, resetIndex: (CurrentState != BodyState.Move));

                Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
                bodyAnimator.transform.localScale = bodyScale;
            }
        }

        public void ProcessInput(Vector2 input, ArmState armState)
        {
            bool isMoving = input.sqrMagnitude > 0f;
            CurrentState = isMoving ? BodyState.Move : BodyState.Idle;

            switch (armState)
            {
            case ArmState.Fire:
            case ArmState.Reload:
                armAnimator.ChangeAnimation(armState.ToString(), resetIndex: true);
                break;
            }
        }

        private void ChangeToIdle()
        {
            CurrentState = BodyState.Idle;
            armAnimator.ChangeAnimation("Idle");
        }
    }
}