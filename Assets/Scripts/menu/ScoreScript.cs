using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    [SerializeField] private Text pontosUI; // Escrever os pontos na tela.
    [SerializeField] private int Qual; // Para saber qual Recorde é.

    private int pontos;
    private string nome;



    void Update()
    {

        if (PlayerPrefs.GetInt("Recorde"+Qual) > 0 )
        {
            pontos = PlayerPrefs.GetInt("Recorde" + Qual); //Qual o recorde
            //nome = PlayerPrefs.GetString("Name" + Qual);    //Qual o nome
            if (Qual == 1) 
            {
                nome = Qual + "st";

            }
            if (Qual == 2)
            {
                nome = Qual + "nd";

            }
            if (Qual == 3)
            {
                nome = Qual + "rd";

            }
            if (Qual == 4)
            {
                nome = Qual + "th";

            }

            pontosUI.text = nome +" : " + pontos;

        }
        else
        {
            pontosUI.text = "Sem pontuação";


        }

        if(Input.GetKey("escape"))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }




    }




}
