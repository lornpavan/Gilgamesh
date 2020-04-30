using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads next scene from the menu to play...Note: requires to "build" to work.
        //SceneManager.LoadScene("level_0");
    }

    public void QuitGame()
    {
      Application.Quit();
    }
}
