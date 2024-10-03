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

    private static readonly string[] SuffixArray = { "_Down", "Right", "Up", };

    [SerializeField]
    private SpriteAnimator bodyAnimator;
    [SerializeField]
    private SpriteAnimator armAnimator;

    private State _currentState;
    private int _defaultAnimationIndex;

    private void Start()
    {
        _currentState = (State)System.Enum.Parse(typeof(State), bodyAnimator.CurrentAnimationName);
        _defaultAnimationIndex = -1;
    }

    private void Update()
    {
        Vector2 mousePosition = RieslingUtils.ExMouse.GetMouseWorldPosition();

        Vector2 diff = mousePosition - (Vector2)transform.position;
        float angle = Vector2.Angle(Vector2.down, diff);

        ChangeArmDirection();
        void ChangeArmDirection()
        {
            armAnimator.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            bodyAnimator.ChangeAnimation("Idle_Down");
        }
               if (Input.GetKeyDown(KeyCode.X))
        {
            bodyAnimator.ChangeAnimation("Idle_Up");
        }

        /*
        ChangeBodyDirection();
        void ChangeBodyDirection()
        {
            int animationIndex = Mathf.Clamp(Mathf.FloorToInt(angle / 180f * 3f), 0, SuffixArray.Length - 1);

            if (_defaultAnimationIndex != animationIndex)
            {
                string animationName = _currentState.ToString();
                if (_currentState == State.Idle || _currentState == State.Move)
                {
                    animationName += SuffixArray[animationIndex];
                }

                bodyAnimator.Renderer.sprite = _bodySpriteTemp[animationIndex];

                Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
                bodyAnimator.transform.localScale = bodyScale;
            }
        }
        */
    }
}
