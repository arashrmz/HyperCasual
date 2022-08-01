using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class UIManager : Singleton<UIManager>
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private LevelIndicator levelIndicator;
    [SerializeField] private TextMeshProUGUI gemText;

    [Header("Lose Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button skipLevelButton;

    [Header("Win Panel")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button doublePrizeButton;
    [SerializeField] private TextMeshProUGUI gemsCollectedInLevel;

    private void Start()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnWinner += OnWinner;
        GameManager.Instance.OnGameStarted += OnGameStarted;

        AddMainMenuListeners();
        AddLosePanelListeners();
        AddWinPanelListeners();

        LoadGemText();
    }

    private void AddLosePanelListeners()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
        skipLevelButton.onClick.AddListener(() => GameManager.Instance.SkipLevel());
    }

    public void SetLevel(int currentLevel)
    {
        levelIndicator.LoadLevel(currentLevel + 1);
    }

    private void AddWinPanelListeners()
    {
        nextLevelButton.onClick.AddListener(() => GameManager.Instance.LoadNextLevel());
        doublePrizeButton.onClick.AddListener(() => GameManager.Instance.DoublePrize());
    }

    private void AddMainMenuListeners()
    {
        settingsButton.onClick.AddListener(OpenSettings);
        shopButton.onClick.AddListener(OpenShop);
    }

    private void OpenShop()
    {
        throw new NotImplementedException();
    }

    private void OpenSettings()
    {
        throw new NotImplementedException();
    }

    private void OnGameStarted()
    {
        mainMenu.SetActive(false);
        levelIndicator.gameObject.SetActive(false);
    }

    private async void OnWinner()
    {
        await Task.Delay(1000);
        gemsCollectedInLevel.text = $"You collected {GameManager.Instance.GemsCollected} gems";
        levelIndicator.gameObject.SetActive(true);
        winPanel.SetActive(true);
    }

    private async void OnGameOver()
    {
        await Task.Delay(1000);
        levelIndicator.gameObject.SetActive(true);
        gameOverPanel.SetActive(true);
    }

    private void OnGemCollected()
    {

    }

    private void LoadGemText()
    {
        gemText.text = $"{GameManager.Instance.TotalGems} Gems";
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= OnGameOver;
        GameManager.Instance.OnWinner -= OnWinner;
        GameManager.Instance.OnGameStarted -= OnGameStarted;
    }
}
