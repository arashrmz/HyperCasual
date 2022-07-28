using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI candyText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject startupText;

    private void Start()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
    }

    public void StartGame()
    {
        startupText.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void UpdateGemText()
    {
        candyText.text = $"{GameManager.Instance.GemsCollected} Candies";
    }
}
