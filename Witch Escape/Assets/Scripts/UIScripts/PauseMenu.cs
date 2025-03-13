using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SCStartScreen");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
