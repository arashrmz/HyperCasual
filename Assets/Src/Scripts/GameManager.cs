using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyperCasual.Assets.Src.Scripts.Player;
using Lean.Touch;
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
    [SerializeField] private GameObject[] levelPrefabs;

    public int KeysOwned { get => _keysOwned; }
    public int GemsCollected { get => _gemsCollected; } //gems collected in current level
    public int TotalGems { get => PlayerPrefs.GetInt("TotalGems", 0); }

    //Events
    //triggered when gem is collected
    public event Action OnGemCollected;
    public event Action OnKeyCollected;
    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action OnWinner;


    private void Start()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
        var currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        var currentLevelPrefab = levelPrefabs[currentLevel];
        var currentLevelObject = Instantiate(currentLevelPrefab, Vector3.zero, Quaternion.identity);
        UIManager.Instance.SetLevel(currentLevel);
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (finger.IsOverGui || _isGameStarted)
        {
            return;
        }
        StartGame();
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
        OnKeyCollected?.Invoke();
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
        PlayerPrefs.SetInt("TotalGems", TotalGems + _gemsCollected);
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 0) + 1);
        OnWinner?.Invoke();
    }

    private void Lose()
    {
        if (_isGameOver)
            return;
        _isGameOver = true;
        OnGameOver?.Invoke();
    }

    public void DoublePrize()
    {
        //watch ad here
        //ad gems collected one more time (total of 2 times)
        PlayerPrefs.SetInt("TotalGems", TotalGems + _gemsCollected);
    }

    public void SkipLevel()
    {
        //watch ad here
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 0) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
    }
}
