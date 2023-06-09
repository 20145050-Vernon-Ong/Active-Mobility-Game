using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MapLoaderScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TMP_Text progressText;
    public GameObject player;
    public GameObject nextLevel;

    void Update()
    {
        if (player.transform.position.x > 1.49 && player.transform.position.x < 3.61)
        {
            nextLevel.SetActive(true);
        }
    }
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
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

}
