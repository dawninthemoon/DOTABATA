using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteEffect : EffectBase
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    [Range(1, 60)]
    public int framePerSec = 10;

    //[HideInInspector]
    public bool repeat = true;

    //[HideInInspector]
    public bool autoRelease = true;
    [HideInInspector]
    public UnityEvent endCallback;


    int _index = 0;
    float _elapsedTime = 0;

    private bool _activated = false;


    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        _index = 0;
        _elapsedTime = 0;
        spriteRenderer.enabled = true;
        _activated = true;

        UpdateSprite();
    }

    public override void Stop()
    {
        spriteRenderer.enabled = false;
        _activated = false;

        base.Stop();

        if (autoRelease)
        {
            Release();
        }
    }

    public override void Release()
    {
        _activated = false;
        base.Release();
    }

    protected virtual void Update()
    {
        if (!_activated)
            return;
        _elapsedTime += Time.deltaTime;

        float tick = 1 / (float)framePerSec;

        while(_elapsedTime > tick)
        {
            _index++;
            _elapsedTime -= tick;

            UpdateSprite();

            if (!_activated)
            {
                _elapsedTime = 0;
                return;
            }
        }
    }

    private void UpdateSprite()
    {
        if (sprites.Length <= 0)
        {
            if (!repeat)
            {
                if (autoRelease)
                    Release();
                else
                    Stop();
                endCallback?.Invoke();
                return;
            }
            else
            {
                return;
            }
        }

        if (!repeat && _index >= sprites.Length)
        {
            if (autoRelease)
                Release();
            else
                Stop();
            endCallback?.Invoke();
        }

        _index = _index % sprites.Length;

        spriteRenderer.sprite = sprites[_index];
    }
}
