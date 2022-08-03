using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop tabs")]
    [SerializeField] private GameObject charactersContainer;
    [SerializeField] private GameObject skatesContainer;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button charactersTabButton;
    [SerializeField] private Button skatesTabButton;


    [Header("Shop items")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private Sprite equipedSprite;
    [SerializeField] private Sprite notEquipedSprite;

    private bool _isCharactersTabActive = true;
    private int _currentCharacterIndex = 0;
    private int _currentSkateIndex = 0;

    private void Start()
    {
        OpenCharatersTab();

        charactersTabButton.onClick.AddListener(() =>
        {
            OpenCharatersTab();
        });

        skatesTabButton.onClick.AddListener(() =>
        {
            OpenSkatesTab();
        });

        AddCharactersButtonListeners();
        AddSkatesButtonListeners();

        _currentCharacterIndex = PlayerPrefs.GetInt("CurrentCharacter", 0);
        _currentSkateIndex = PlayerPrefs.GetInt("CurrentSkate", 0);

        UpdateCurrentCharacterButton();
        UpdateCurrentSkateButton();
    }

    private void UpdateCurrentCharacterButton()
    {
        int index = 0;
        foreach (var characterButton in charactersContainer.GetComponentsInChildren<Button>())
        {
            int characterIndex = index;
            if (characterIndex == _currentCharacterIndex)
            {
                characterButton.GetComponent<Image>().sprite = equipedSprite;
            }
            else
            {
                characterButton.GetComponent<Image>().sprite = notEquipedSprite;
            }
            index++;
        }
    }

    private void UpdateCurrentSkateButton()
    {
        int index = 0;
        foreach (var skateButton in skatesContainer.GetComponentsInChildren<Button>())
        {
            int skateIndex = index;
            if (skateIndex == _currentSkateIndex)
            {
                skateButton.GetComponent<Image>().sprite = equipedSprite;
            }
            else
            {
                skateButton.GetComponent<Image>().sprite = notEquipedSprite;
            }
            index++;
        }
    }

    private void AddSkatesButtonListeners()
    {
        int index = 0;
        foreach (var skateButton in skatesContainer.GetComponentsInChildren<Button>())
        {
            int skateIndex = index;
            skateButton.onClick.AddListener(() =>
            {
                SetSelectedSkate(skateIndex);
            });
            index++;
        }
    }

    private void SetSelectedSkate(int index)
    {
        equipButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Selected skate: " + index);
        //TODO: set selected character preview image
        //if player owns character, enable equip button
        if (PlayerPrefs.GetInt("CurrentSkate", 0) == index)
        {
            equipButton.GetComponent<Button>().interactable = false;
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Skate" + index, 0) == 1)
        {
            int skateIndex = index;
            equipButton.GetComponent<Button>().onClick.AddListener(() =>
            {

                EquipSkate(skateIndex);
            });
            equipButton.GetComponent<Button>().interactable = true;
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            int skateIndex = index;

            buyButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuySkate(skateIndex);
            });
            equipButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }

    private void AddCharactersButtonListeners()
    {
        int index = 0;
        foreach (var characterButton in charactersContainer.GetComponentsInChildren<Button>())
        {
            int characterIndex = index;
            characterButton.onClick.AddListener(() =>
            {
                SetSelectedCharacter(characterIndex);
            });
            index++;
        }
    }

    private void SetSelectedCharacter(int index)
    {
        equipButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Selected character: " + index);
        //TODO: set selected character preview image
        //if player owns character, enable equip button
        if (PlayerPrefs.GetInt("CurrentCharacter", 0) == index)
        {
            equipButton.GetComponent<Button>().interactable = false;
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Character" + index, 0) == 1)
        {
            int characterIndex = index;
            equipButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                EquipCharacter(characterIndex);
            });
            equipButton.GetComponent<Button>().interactable = true;
            equipButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            int characterIndex = index;
            buyButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyCharacter(characterIndex);
            });
            equipButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }

    private void OpenCharatersTab()
    {
        _isCharactersTabActive = true;
        scrollRect.content = charactersContainer.GetComponent<RectTransform>();
        charactersTabButton.GetComponent<Button>().interactable = false;
        skatesTabButton.GetComponent<Button>().interactable = true;
        charactersContainer.SetActive(true);
        skatesContainer.SetActive(false);
        scrollRect.content = charactersContainer.GetComponent<RectTransform>();
    }

    private void OpenSkatesTab()
    {
        _isCharactersTabActive = false;
        scrollRect.content = skatesContainer.GetComponent<RectTransform>();
        charactersTabButton.GetComponent<Button>().interactable = true;
        skatesTabButton.GetComponent<Button>().interactable = false;
        charactersContainer.SetActive(false);
        skatesContainer.SetActive(true);
        scrollRect.content = skatesContainer.GetComponent<RectTransform>();
    }

    public void BuyCharacter(int index)
    {
        if (PlayerPrefs.GetInt("Character" + index, 0) == 0)
        {
            PlayerPrefs.SetInt("Character" + index, 1);
            PlayerPrefs.SetInt("CurrentCharacter", index);
            _currentCharacterIndex = index;
            UpdateCurrentCharacterButton();
        }
    }

    public void BuySkate(int index)
    {
        if (PlayerPrefs.GetInt("Skate" + index, 0) == 0)
        {
            PlayerPrefs.SetInt("Skate" + index, 1);
            PlayerPrefs.SetInt("CurrentSkate", index);
            _currentSkateIndex = index;
            UpdateCurrentSkateButton();
        }
    }

    public void EquipCharacter(int index)
    {
        if (PlayerPrefs.GetInt("Character" + index, 0) == 1)
        {
            PlayerPrefs.SetInt("CurrentCharacter", index);
            _currentCharacterIndex = index;
            UpdateCurrentCharacterButton();
        }
    }

    public void EquipSkate(int index)
    {
        if (PlayerPrefs.GetInt("Skate" + index, 0) == 1)
        {
            PlayerPrefs.SetInt("CurrentSkate", index);
            _currentSkateIndex = index;
            UpdateCurrentSkateButton();
        }
    }

    public void CloseShop()
    {
        UIManager.Instance.CloseShop();
    }
}
