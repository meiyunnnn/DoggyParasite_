using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Quitgame()
        {
        Application.Quit();
        }
}
