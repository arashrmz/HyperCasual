using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI keyText;
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

    public void UpdateKeyText()
    {
        keyText.text = $"{GameManager.Instance.KeysOwned} keys";
    }
}
