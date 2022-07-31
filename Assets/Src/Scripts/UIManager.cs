using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject startupText;

    private void Start()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
        GameManager.Instance.OnGemCollected += OnGemCollected;
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnWinner += OnWinner;
        GameManager.Instance.OnGameStarted += OnGameStarted;
    }

    private void OnGameStarted()
    {
        startupText.SetActive(false);
    }

    private async void OnWinner()
    {
        await Task.Delay(1000);
        winPanel.SetActive(true);
    }

    private async void OnGameOver()
    {
        await Task.Delay(1000);
        gameOverPanel.SetActive(true);
    }

    private void OnGemCollected()
    {
        gemText.text = $"{GameManager.Instance.GemsCollected} Gems";
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGemCollected -= OnGemCollected;
        GameManager.Instance.OnGameOver -= OnGameOver;
        GameManager.Instance.OnWinner -= OnWinner;
        GameManager.Instance.OnGameStarted -= OnGameStarted;
    }
}
