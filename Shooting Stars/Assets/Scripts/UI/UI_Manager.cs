using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextDefeat;
    public TMPro.TextMeshProUGUI TextSuccess;
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamesScene");
    }

    private void Update()
    {
        if (EnemyInstanciator.Instance.IsFighting == false)
        {
            TextDefeat.text = PlayerScore.Instance.Points.ToString();
            TextSuccess.text = PlayerScore.Instance.Points.ToString();
        }
    }

}
