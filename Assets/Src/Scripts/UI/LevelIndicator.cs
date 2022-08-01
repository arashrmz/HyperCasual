using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private Image[] levelIndicators;
    [SerializeField] private RectTransform levelIndicatorContainer;
    [SerializeField] private float levelIndicatorSize = 100f;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        LoadLevel(6);
    }

    public void LoadLevel(int level)
    {
        levelText.text = "Level " + level;

        level = level - 1;

        //change passed levels color to green
        for (int i = 0; i < levelIndicators.Length; i++)
        {
            if (i < level)
            {
                levelIndicators[i].color = Color.green;
            }
        }

        //change container position to center on current level
        float levelPosition = level * levelIndicatorSize;
        levelIndicatorContainer.localPosition -= new Vector3(levelPosition, 0, 0);
    }
}
