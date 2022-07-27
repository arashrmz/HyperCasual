using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int _keysCollected = 0;
    private int _keysOwned = 0;
    private bool _isGameStarted = false;

    [SerializeField] private PlayerManager playerManager;

    public int KeysOwned { get => _keysOwned; }

    private void Start()
    {
        UIManager.Instance.UpdateKeyText();
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isGameStarted = true;
                UIManager.Instance.StartGame();
                playerManager.StartGame();
            }
        }
    }

    public void OnKeyCollected()
    {
        _keysCollected++;
        _keysOwned++;
        UIManager.Instance.UpdateKeyText();
    }

    public void OnEnteredDoorRange(Door door)
    {
        if (_keysOwned > 0)
        {
            _keysOwned--;
            UIManager.Instance.UpdateKeyText();
            door.Open();
            Debug.Log("Door opened");
        }
    }

    public void OnEnteredDoor(Door door)
    {
        if (door.IsOpen)
        {
            door.EnterDoor();
        }
        else
        {
            playerManager.Crash();
        }
    }

    public void OnEnteredFinalDoor()
    {
        UIManager.Instance.Win();
    }

    public async void OnFallDown()
    {
        // Debug.Log("You lose");
        await Task.Delay(1000);
        UIManager.Instance.GameOver();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
