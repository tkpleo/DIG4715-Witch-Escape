using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SweetCarnageMain");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("SCStartScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
