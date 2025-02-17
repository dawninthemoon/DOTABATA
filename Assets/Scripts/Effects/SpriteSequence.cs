using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteSequence : MonoBehaviour
{
    //[SerializeField]
    public SpriteRenderer spriteRenderer;


    [SerializeField]
    private Sprite[] sprites;

    [Range(1, 60)]
    public int framePerSec = 10;
    [SerializeField]
    private bool playOnEnable = true;

    [HideInInspector]
    public bool repeat = true;

    [HideInInspector]
    public bool autoDisable = true;
    [HideInInspector]
    public UnityEvent endCallback;

    #region event
    private Action _event;
    private int _eventFrame;
    #endregion

    int _index = 0;
    float _elapsedTime = 0;

    private bool _activated = false;


    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if(playOnEnable)
        {
            Play();
        }
    }

    public void Play()
    {
        _index = 0;
        _elapsedTime = 0;
        spriteRenderer.enabled = true;
        _activated = true;
        _calledEventFrames.Clear();
        UpdateSprite();
    }
    public void Stop()
    {
        spriteRenderer.enabled = false;
        _activated = false;
    }

    private void Update()
    {
        if (!_activated)
            return;
        _elapsedTime += Time.deltaTime;

        float tick = 1 / (float)framePerSec;

        while (_elapsedTime > tick)
        {
            _index++;
            _elapsedTime -= tick;

            UpdateSprite();

            if (_event != null && _eventFrame == _index)
            {
                _event.Invoke();
            }

            if(!_activated)
            {
                _elapsedTime = 0;
                return;
            }
        }
    }
    public void SetEventFrame(Action e, int frame)
    {
        _event = e;
        _eventFrame = frame;
    }
    public void ClearEvent()
    {
        _event = null;
    }


    private void UpdateSprite()
    {
        if(sprites.Length <= 0)
        {
            if(!repeat)
            {
                Stop();
                endCallback?.Invoke();
                return;
            }
            else
            {
                return;
            }
        }

        CheckEvent();

        if (!repeat && _index >= sprites.Length)
        {
            if(autoDisable)
            {
                Stop();
            }
            else
            {
                _activated = false;
            }
            endCallback?.Invoke();
            return;
        }

        _index = _index % sprites.Length;

        spriteRenderer.sprite = sprites[_index];
    }


#region events

#region inspector
    public Combat.SpriteAnimationEventInfo[] events;
#endregion

    private HashSet<int> _calledEventFrames = new HashSet<int>();
    private Dictionary<string, System.Action> _registeredEvents = new Dictionary<string, System.Action>();
    public void SetEventCallback(string eventName, System.Action e)
    {
        if (e != null)
        {
            _registeredEvents[eventName] = e;
        }
        else
        {
            _registeredEvents.Remove(eventName);
        }
    }

    private void CheckEvent()
    {
        if (!_calledEventFrames.Contains(_index) && events != null)
        {
            bool called = false;
            for (int i = 0; i < events.Length; i++)
            {
                var e = events[i];
                if (e.frameIndex == _index)
                {
                    called = true;
                    CallEvent(e.eventName);
                }
            }
            if (called)
            {
                _calledEventFrames.Add(_index);
            }
        }

        if (_index >= sprites.Length)
        {
            _calledEventFrames.Clear();
        }
    }
    private void CallEvent(string eventName)
    {
        if (_registeredEvents.TryGetValue(eventName, out var e))
        {
            e?.Invoke();
        }
    }
#endregion
}
