using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas _loadingScreen;
    [SerializeField] private Slider _progressBar;

    private float _progressBarChangeValueSpeed = 0.05f;

    private void Awake()
    {
        _loadingScreen.gameObject.SetActive(false);
    }

    public void LoadScene(int loadingScene)
    {
        OpenLoadingScreen();

        StartCoroutine(LoadAsync(loadingScene));
    }

    private void OpenLoadingScreen()
    {
        _loadingScreen.gameObject.SetActive(true);
    }

    private IEnumerator LoadAsync(int loadingScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingScene);

        while (!asyncLoad.isDone)
        {
            _progressBar.value = Mathf.MoveTowards(_progressBar.value, asyncLoad.progress, _progressBarChangeValueSpeed);

            yield return null;
        }
    }
}
