using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Animator skateAnimator;

    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Moving = Animator.StringToHash("Moving");
    public static readonly int Start = Animator.StringToHash("Start");
    public static readonly int Crash = Animator.StringToHash("Crash");
    public static readonly int Falling = Animator.StringToHash("Falling");

    public void OnIdle()
    {
        characterAnimator.CrossFade(Idle, 0.1f);
        skateAnimator.CrossFade(Idle, 0.1f);
    }

    public void OnStart()
    {
        characterAnimator.CrossFade(Start, 0.1f);
        skateAnimator.CrossFade(Start, 0.1f);
    }

    public void OnMoving()
    {
        characterAnimator.CrossFade(Moving, 0.1f);
        skateAnimator.CrossFade(Moving, 0.1f);
    }

    public void OnCrash()
    {
        characterAnimator.CrossFade(Crash, 0.1f);
        skateAnimator.CrossFade(Crash, 0.1f);
    }

    public void OnFalling()
    {
        characterAnimator.CrossFade(Falling, 0.3f);
        skateAnimator.CrossFade(Idle, 0.3f);
    }
}
