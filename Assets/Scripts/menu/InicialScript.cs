using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicialScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MenuPrincipal");
        }


    }
}
