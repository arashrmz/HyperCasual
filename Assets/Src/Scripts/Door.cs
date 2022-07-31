using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen = false;

    [SerializeField] private bool isFinalDoor = false;

    public bool IsOpen { get => _isOpen; }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger("Open");
        _isOpen = true;
    }

    public void EnterDoor()
    {
        if (isFinalDoor)
            GameManager.Instance.EnteredFinalDoor();
    }

}
