using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Freeze,
        Down,
    }

    private static readonly string[] SuffixArray = { "_Down", "_Right", "_Up", };

    [SerializeField]
    private SpriteAnimator bodyAnimator;
    [SerializeField]
    private SpriteAnimator armAnimator;
    [field: SerializeField]
    public Transform BulletTransform { get; private set; }
    public State CurrentState { get; private set; }
    private int _defaultAnimationIndex;

    private void Start()
    {
        CurrentState = State.Idle;
        _defaultAnimationIndex = -1;

        armAnimator.SetEndCallback("Fire", ChangeToIdle);
    }

    private void Update()
    {
        Vector2 mousePosition = RieslingUtils.ExMouse.GetMouseWorldPosition();

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

            if (CurrentState == State.Idle || CurrentState == State.Move)
            {
                animationName += SuffixArray[_defaultAnimationIndex];
            }
            bodyAnimator.ChangeAnimation(animationName, forceReset: (CurrentState != State.Move));

            Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
            bodyAnimator.transform.localScale = bodyScale;
        }
    }

    public void ProcessInput(Vector2 input, bool fired)
    {
        bool isMoving = input.sqrMagnitude > 0f;
        CurrentState = isMoving ? State.Move : State.Idle;

        if (fired)
        {
            armAnimator.ChangeAnimation("Fire", forceReset: true);
        }
    }

    private void ChangeToIdle()
    {
        CurrentState = State.Idle;
        armAnimator.ChangeAnimation("Idle");
    }
}
