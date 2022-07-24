using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int _keysCollected = 0;
    private bool _isDoorOpen;

    [SerializeField] private int keysToCollect = 5;

    public int TotalKeys { get => keysToCollect; }

    private void Start()
    {
        UIManager.Instance.UpdateKeyText(_keysCollected);
    }

    public void PlayerDiscovered()
    {
        Debug.Log("Player discovered");
    }

    public void OnKeyCollected()
    {
        _keysCollected++;
        UIManager.Instance.UpdateKeyText(_keysCollected);
        if (_keysCollected == keysToCollect)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        _isDoorOpen = true;
        Debug.Log("Door opened");
    }

    public void OnEnteredDoor()
    {
        if (_isDoorOpen)
        {
            Debug.Log("You win!");
            UIManager.Instance.Win();
        }
    }

    public void OnDiscoveredByEnemy()
    {
        Debug.Log("You lose");
        UIManager.Instance.GameOver();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
