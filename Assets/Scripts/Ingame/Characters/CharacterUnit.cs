using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private CharacterRenderer characterRenderer;

    private void Update()
    {
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
            Vector3 moveVector = input.normalized * moveSpeed; 
            transform.position += moveVector * Time.deltaTime;
        }

        characterRenderer.ProcessInput(input, Input.GetMouseButtonDown(0));
    }
}
