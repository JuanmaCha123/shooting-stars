using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GamesScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
