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
    [SerializeField] private GameObject[] charactersPrefabs;
    [SerializeField] private GameObject[] skatesPrefabs;

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
    public event Action OnCrash;
    public event Action OnOpenDoor;

    private void Start()
    {
        InitFirstRun();
        LoadCharacter();
        LeanTouch.OnFingerDown += HandleFingerDown;
        var currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        var currentLevelPrefab = levelPrefabs[currentLevel];
        var currentLevelObject = Instantiate(currentLevelPrefab, Vector3.zero, Quaternion.identity);
        UIManager.Instance.SetLevel(currentLevel);
    }

    private void InitFirstRun()
    {
        if (PlayerPrefs.GetInt("FirstRun", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.SetInt("TotalGems", 0);
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.SetInt("CurrentCharacter", 0);
            PlayerPrefs.SetInt("CurrentSkate", 0);
            PlayerPrefs.SetInt("Character0", 1);
            PlayerPrefs.SetInt("Skate0", 1);
        }
    }

    private void LoadCharacter()
    {
        //load character
        var currentCharIndex = PlayerPrefs.GetInt("CurrentCharacter", 0);
        var currentCharObject = Instantiate(charactersPrefabs[currentCharIndex], Vector3.zero, Quaternion.identity);

        //load skate
        var currentSkateIndex = PlayerPrefs.GetInt("CurrentSkate", 0);
        var currentSkateObject = Instantiate(skatesPrefabs[currentSkateIndex], Vector3.zero, Quaternion.identity);

        //place skate under player
        currentSkateObject.transform.parent = currentCharObject.transform.GetChild(1).GetChild(0);
        currentSkateObject.transform.localPosition = Vector3.zero;

        currentCharObject.transform.parent = playerManager.transform.parent;
        currentCharObject.transform.localPosition = Vector3.zero;

        playerManager.ReplacePlayerModel(currentCharObject, currentSkateObject);
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
            OnOpenDoor?.Invoke();
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
            OnCrash?.Invoke();
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
