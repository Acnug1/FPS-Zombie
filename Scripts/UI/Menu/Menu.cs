using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public abstract class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _errorSound;
    [SerializeField] private GameObject _touchscreenControl;

    private AudioSource _audioSource;
    protected int CurrentScene;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    protected abstract void SelectFirstButton();

    public void Restart()
    {
        PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(CurrentScene);
    }

    public void ReturnToMainMenu()
    {
        PlayClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        PlayClickSound();
        Application.Quit();
    }

    protected void SetTouchActive(bool isOpenTouchscreenPanel)
    {
        if (_touchscreenControl)
            _touchscreenControl.SetActive(isOpenTouchscreenPanel);
    }

    protected void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound, Settings.Volume);
    }

    protected void PlayErrorSound()
    {
        _audioSource.PlayOneShot(_errorSound, Settings.Volume);
    }
}