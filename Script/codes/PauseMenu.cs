using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public Scene pauseMenuUI;
    public gcs_menu gcs;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gcs.PlayClick();
        //pauseMenuUI.SetActive(false);
        SceneManager.UnloadSceneAsync("Menu");
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        //pauseMenuUI.SetActive(true);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        gcs.PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        gcs.PlayClick();
        Application.Quit();
    }

}
