using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip collectKeySound;
    [SerializeField] private AudioClip collectGemSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip winSound;

    void Start()
    {
        //Events
        GameManager.Instance.OnGameOver += PlayGameOverSound;
        GameManager.Instance.OnWinner += PlayWinSound;
        GameManager.Instance.OnKeyCollected += PlayCollectKeySound;
        GameManager.Instance.OnGemCollected += PlayCollectGemSound;
    }


    private void PlayGameOverSound()
    {
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }

    private void PlayWinSound()
    {
        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
    }

    private void PlayCollectKeySound()
    {
        AudioSource.PlayClipAtPoint(collectKeySound, Camera.main.transform.position);
    }

    private void PlayCollectGemSound()
    {
        AudioSource.PlayClipAtPoint(collectGemSound, Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= PlayGameOverSound;
        GameManager.Instance.OnWinner -= PlayWinSound;
        GameManager.Instance.OnKeyCollected -= PlayCollectKeySound;
        GameManager.Instance.OnGemCollected -= PlayCollectGemSound;
    }
}
