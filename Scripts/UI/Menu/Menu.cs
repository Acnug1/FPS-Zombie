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

    protected AudioSource AudioSource;
    protected int CurrentScene;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
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
        AudioSource.PlayOneShot(_clickSound, Settings.Volume);
    }

    protected void PlayErrorSound()
    {
        AudioSource.PlayOneShot(_errorSound, Settings.Volume);
    }
}