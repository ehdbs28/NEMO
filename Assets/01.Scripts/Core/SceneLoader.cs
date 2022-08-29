using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [SerializeField] private Image _loadingPanel;
    [SerializeField] private Slider _loadingSlider;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        _loadingPanel.gameObject.SetActive(true);
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOper.isDone)
        {
            yield return null;
            _loadingSlider.value = asyncOper.progress;
        }
    }
}
