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
    private int _gemsCollected = 0;
    private bool _isGameStarted = false;
    private bool _isGameOver = false;
    private bool _isWinner = false;

    [SerializeField] private PlayerManager playerManager;

    public int KeysOwned { get => _keysOwned; }
    public int GemsCollected { get => _gemsCollected; }

    private void Start()
    {
        UIManager.Instance.UpdateGemText();
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
    }

    public void OnGemCollected()
    {
        _gemsCollected++;
        UIManager.Instance.UpdateGemText();
    }

    public void OnEnteredDoorRange(Door door)
    {
        if (_keysOwned > 0)
        {
            _keysOwned--;
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
        if (_isWinner)
            return;
        _isWinner = true;
        UIManager.Instance.Win();
        playerManager.StopPlayer();
    }

    public async void OnFallDown()
    {
        if (_isGameOver)
            return;
        Camera.main.GetComponent<CameraFollow>().ShouldFollow = false;
        _isGameOver = true;
        await Task.Delay(1000);
        UIManager.Instance.GameOver();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
