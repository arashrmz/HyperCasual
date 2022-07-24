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

    private void Start()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void UpdateKeyText(int keysCollected)
    {
        Debug.Log("Updating key text");
        keyText.text = $"{keysCollected}/{GameManager.Instance.TotalKeys} keys";
    }
}
