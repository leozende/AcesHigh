using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    public void OnClickSelectGame()
    {
        SceneManager.LoadScene("Fase1");
    }
}
