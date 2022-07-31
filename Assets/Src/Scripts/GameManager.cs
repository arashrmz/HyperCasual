using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperCasual.Assets.Src.Scripts.Player;
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


    //Events
    //triggered when gem is collected
    public event Action OnGemCollected;
    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action OnWinner;

    private void Start()
    {

    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        _isGameStarted = true;
        OnGameStarted?.Invoke();
    }

    public void CollectKey()
    {
        _keysCollected++;
        _keysOwned++;
    }

    public void CollectGem()
    {
        _gemsCollected++;
        OnGemCollected?.Invoke();
    }

    public void EnteredDoorRange(Door door)
    {
        if (_keysOwned > 0)
        {
            _keysOwned--;
            door.Open();
            Debug.Log("Door opened");
        }
    }

    public void EnteredDoor(Door door)
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

    public void EnteredFinalDoor()
    {
        Win();
    }

    public void FallDown()
    {
        Lose();
    }

    private void Win()
    {
        if (_isWinner)
            return;
        _isWinner = true;
        OnWinner?.Invoke();
    }

    private void Lose()
    {
        if (_isGameOver)
            return;
        _isGameOver = true;
        OnGameOver?.Invoke();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
