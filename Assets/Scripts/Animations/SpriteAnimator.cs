using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private void OnEnable()
    {
        if (spriteRenderer is null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
