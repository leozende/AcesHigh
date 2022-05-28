using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private Text vidasUI;// Arrastar a vida aqui
    [SerializeField] private AudioSource End;

    public int vidas;// Dar acesso ao script airplane.

    private bool Final = true; // Para que a música final não fique se repetindo

    private PointScript ptScript; // Para se comunicar com o script

    private void Start()
    {

        ptScript = GameObject.Find("Tela").GetComponent<PointScript>();

    }

    void Update()
    {   
        vidasUI.text = "Vidas: " + vidas ;
        
        ptScript.Life = vidas;

        if (vidas == 0 && Final == true)
        {
            
            End.Play();
            Final = false;



        }

    }

}
