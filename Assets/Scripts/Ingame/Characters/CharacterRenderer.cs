using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Fire,
        Freeze,
        Down,
    }

    private static readonly string[] SuffixArray = { "_Down", "_Right", "_Up", };

    [SerializeField]
    private SpriteAnimator bodyAnimator;
    [SerializeField]
    private SpriteAnimator armAnimator;

    private State _currentState;
    private int _defaultAnimationIndex;

    private void Start()
    {
        _currentState = State.Idle;
        _defaultAnimationIndex = -1;

        armAnimator.SetEndCallback("Fire", () => armAnimator.ChangeAnimation("Idle"));
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

            string animationName = _currentState.ToString();
            if (_defaultAnimationIndex != animationIndex)
            {
                _defaultAnimationIndex = animationIndex;
            }

            if (_currentState == State.Idle || _currentState == State.Move)
            {
                animationName += SuffixArray[_defaultAnimationIndex];
            }
            bodyAnimator.ChangeAnimation(animationName, resetIndex: (_currentState != State.Move));

            Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
            bodyAnimator.transform.localScale = bodyScale;
        }
    }

    public void ProcessInput(Vector2 input, bool fired)
    {
        bool isMoving = input.sqrMagnitude > 0f;
        _currentState = isMoving ? State.Move : State.Idle;

        if (fired)
        {
            armAnimator.ChangeAnimation("Fire");
        }
    }
}
