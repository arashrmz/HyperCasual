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
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip openDoorSound;

    void Start()
    {
        //Events
        GameManager.Instance.OnGameOver += PlayGameOverSound;
        GameManager.Instance.OnWinner += PlayWinSound;
        GameManager.Instance.OnKeyCollected += PlayCollectKeySound;
        GameManager.Instance.OnGemCollected += PlayCollectGemSound;
        GameManager.Instance.OnCrash += PlayCrashSound;
        GameManager.Instance.OnOpenDoor += PlayOpenDoorSound;
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

    private void PlayCrashSound()
    {
        AudioSource.PlayClipAtPoint(crashSound, Camera.main.transform.position);
    }

    private void PlayOpenDoorSound()
    {
        AudioSource.PlayClipAtPoint(openDoorSound, Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= PlayGameOverSound;
        GameManager.Instance.OnWinner -= PlayWinSound;
        GameManager.Instance.OnKeyCollected -= PlayCollectKeySound;
        GameManager.Instance.OnGemCollected -= PlayCollectGemSound;
        GameManager.Instance.OnCrash -= PlayCrashSound;
        GameManager.Instance.OnOpenDoor -= PlayOpenDoorSound;
    }
}
