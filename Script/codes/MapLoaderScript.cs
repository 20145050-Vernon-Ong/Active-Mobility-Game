using System.Collections;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MapLoaderScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TMP_Text progressText;
    void Start()
    {
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            PlayerPrefs.SetInt("mobile", 1); 
        } else
        {
            PlayerPrefs.SetInt("mobile", 0);
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

}
