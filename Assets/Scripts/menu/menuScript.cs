using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public void OnClickStartGame()
    {
        SceneManager.LoadScene("MenuEscolha");
    }
    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void OnClickScoreGame()
    {
        SceneManager.LoadScene("Score");
    }

}
