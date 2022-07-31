using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using System;

public class InputManager : Singleton<InputManager>
{

    private SwipeDirection _swipeDirection = SwipeDirection.None;
    public SwipeDirection SwipeDirection
    {
        get
        {
            var temp = _swipeDirection;
            _swipeDirection = SwipeDirection.None;
            return temp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LeanTouch.OnFingerSwipe += HandleFingerSwipe;

    }

    private void HandleFingerSwipe(LeanFinger finger)
    {
        // Debug.Log(finger.SwipeScreenDelta);
        var swipe = finger.SwipeScreenDelta;
        if (MathF.Abs(swipe.x) > MathF.Abs(swipe.y))
        {
            if (swipe.x > 100f)
            {
                Debug.Log("Swipe Right");
                _swipeDirection = SwipeDirection.Right;
            }
            else
            {
                Debug.Log("Swipe Left");
                _swipeDirection = SwipeDirection.Left;
            }
        }
        else
        {
            if (swipe.y > 100f)
            {
                Debug.Log("Swipe Up");
                _swipeDirection = SwipeDirection.Up;
            }
            else
            {
                Debug.Log("Swipe Down");
                _swipeDirection = SwipeDirection.Down;
            }
        }
    }


    private void OnDestroy()
    {
        LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
    }

    // Update is called once per frame
    void Update()
    {
        //detect swipe here
        var left = Input.GetKeyDown(KeyCode.LeftArrow);
        var right = Input.GetKeyDown(KeyCode.RightArrow);
        var up = Input.GetKeyDown(KeyCode.UpArrow);
        var down = Input.GetKeyDown(KeyCode.DownArrow);

        if (!left && !right && !up && !down)
        {
            return;
        }

        if (left)
        {
            _swipeDirection = SwipeDirection.Left;
        }
        else if (right)
        {
            _swipeDirection = SwipeDirection.Right;
        }
        else if (up)
        {
            _swipeDirection = SwipeDirection.Up;
        }
        else if (down)
        {
            _swipeDirection = SwipeDirection.Down;
        }

    }
}

public enum SwipeDirection
{
    None,
    Left,
    Right,
    Up,
    Down
}