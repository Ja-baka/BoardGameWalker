using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        int nextSceneInex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneInex);
    }

    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
