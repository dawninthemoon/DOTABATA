using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat
{
    public class CharacterRenderer : MonoBehaviour
    {
        public enum BodyState
        {
            Undefined,
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
        private BodyState _prevState;

        private void Start()
        {
            _prevState = BodyState.Undefined;
            CurrentState = BodyState.Idle;
            _defaultAnimationIndex = -1;

            bodyAnimator.ChangeAnimation(BodyState.Idle.ToString(), resetIndex: true);
            armAnimator.SetActiveState(true);
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
                string animationName = CurrentState.ToString();

                if (CurrentState == BodyState.Idle || CurrentState == BodyState.Move)
                {
                    int animationIndex = Mathf.Clamp(Mathf.FloorToInt(angle / 180f * 3f), 0, SuffixArray.Length - 1);
                    if (_defaultAnimationIndex != animationIndex)
                    {
                        _defaultAnimationIndex = animationIndex;
                    }

                    animationName += SuffixArray[_defaultAnimationIndex];
                }

                bodyAnimator.ChangeAnimation(animationName, resetIndex: (CurrentState == BodyState.Idle));

                if (CurrentState != BodyState.Down)
                {
                    Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
                    bodyAnimator.transform.localScale = bodyScale;
                }
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

        public void ChangeToDown()
        {
            CurrentState = BodyState.Down;
            armAnimator.SetActiveState(false);
            bodyAnimator.ChangeAnimation(CurrentState.ToString(), resetIndex: true);
        }

        private void ChangeToIdle()
        {
            CurrentState = BodyState.Idle;
            armAnimator.ChangeAnimation("Idle");
        }
    }
}