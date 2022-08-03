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

    public void SetAnimator(Animator newCharacterAnimator, Animator newSkateAnimator)
    {
        characterAnimator = newCharacterAnimator;
        skateAnimator = newSkateAnimator;
    }

    public void PlayIdleAnimation()
    {
        characterAnimator.CrossFade(Idle, 0.1f);
        skateAnimator.CrossFade(Idle, 0.1f);
    }

    public void PlayStartAnimation()
    {
        characterAnimator.CrossFade(Start, 0.1f);
        skateAnimator.CrossFade(Start, 0.1f);
    }

    public void PlayMoveAnimation()
    {
        characterAnimator.CrossFade(Moving, 0.1f);
        skateAnimator.CrossFade(Moving, 0.1f);
    }

    public void PlayCrashAnimation()
    {
        characterAnimator.CrossFade(Crash, 0.1f);
        skateAnimator.CrossFade(Crash, 0.1f);
    }

    public void PlayFallAnimation()
    {
        characterAnimator.CrossFade(Falling, 0.3f);
        skateAnimator.CrossFade(Idle, 0.3f);
    }
}
