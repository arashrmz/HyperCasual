using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private Image[] levelIndicators;
    [SerializeField] private RectTransform levelIndicatorContainer;
    [SerializeField] private float levelIndicatorSize = 110f;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Sprite passedLevelIndicatorSprite;

    public void LoadLevel(int level)
    {
        levelText.text = "Level " + level;

        level = level - 1;

        //change passed levels color to green
        for (int i = 0; i < levelIndicators.Length; i++)
        {
            if (i < level)
            {
                levelIndicators[i].sprite = passedLevelIndicatorSprite;
            }
        }

        //change container position to center on current level
        float levelPosition = level * levelIndicatorSize;
        levelIndicatorContainer.localPosition -= new Vector3(levelPosition, 0, 0);
    }
}
