using System.Collections;
using System.Collections.Generic;
using RieslingUtils;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer bodyRenderer;

    [SerializeField]
    private Transform bodyTransform;
    [SerializeField]
    private Transform armTransform;
    [SerializeField]
    private float moveSpeed;

    private List<Sprite> _bodySpriteTemp;

    private void Awake()
    {
        _bodySpriteTemp = new();
        _bodySpriteTemp.Add(Resources.Load<Sprite>($"{AssetLoader.DummyBodyPathBase}/C_Idle/SPR_Char_Dummy_C_Idle_Down"));
        _bodySpriteTemp.Add(Resources.Load<Sprite>($"{AssetLoader.DummyBodyPathBase}/C_Idle/SPR_Char_Dummy_C_Idle_Right"));
        _bodySpriteTemp.Add(Resources.Load<Sprite>($"{AssetLoader.DummyBodyPathBase}/C_Idle/SPR_Char_Dummy_C_Idle_Up"));
    }

    private void Update()
    {
        Vector2 mousePosition = RieslingUtils.ExMouse.GetMouseWorldPosition();

        Vector2 diff = mousePosition - (Vector2)transform.position;
        float angle = Vector2.Angle(Vector2.down, diff);

        ChangeArmDirection();
        void ChangeArmDirection()
        {
            armTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        ChangeBodyDirection();
        void ChangeBodyDirection()
        {
            int spriteIndex = Mathf.Clamp(Mathf.FloorToInt(angle / 180f * 3f), 0, _bodySpriteTemp.Count - 1);
            bodyRenderer.sprite = _bodySpriteTemp[spriteIndex];

            Vector2 bodyScale = new Vector3(Mathf.Sign(diff.x), 1f, 1f);
            bodyTransform.transform.localScale = bodyScale;
        }

        Vector2 input;
        ControlInput();
        void ControlInput()
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }

        UpdatePosiiton();
        void UpdatePosiiton()
        {
            Vector3 moveVector = input * moveSpeed; 
            transform.position += moveVector * Time.deltaTime;
        }
    }
}
