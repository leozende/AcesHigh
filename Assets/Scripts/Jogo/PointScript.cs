using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
  

    // Este script foi anexado a um GameObject Empty arrastado dentro da Main Camera
    // Este script precisa ficar anexado a um objeto que esteja o tempo todo
    // valendo, como a main camera ou o background

public class PointScript : MonoBehaviour
{
    
    [SerializeField] private Text pontosUI;// Arrastar o score aqui
                                           // Pontos da Main Camera
    [SerializeField] private GameObject Escrita; //Armazena o prefab para escrever


    public int pontos;// Dar acesso ao script airplane.

    public int Life = 3;

    //public string nome; //Dar acesso ao script do nome

    private bool gravado = false; // Para impedir que o recorde seja gravado em todos.

    private bool Fim = true; // Para impedir que o processo de salvar se repita a todo momento.


    void Update()
        {

        GravarPontos();


        

        }

    // Sistema para se gravar 4 recordes, substituindo-os pelo recorde maior.

    void GravarPontos()
    {
        
        pontosUI.text = "" + pontos; //Mostrar pontos


        if (Life == 0 && Fim == true) // O salvamento só ocorrerá no fim.
        {
            

            if (pontos > PlayerPrefs.GetInt("Recorde1")) //Salvando o recorde caso ele seja maior que o primeiro.
            {
                //Instantiate(Escrita, new Vector2(0, 0), Quaternion.identity);
                PlayerPrefs.SetInt("Recorde4", PlayerPrefs.GetInt("Recorde3"));
                PlayerPrefs.SetInt("Recorde3", PlayerPrefs.GetInt("Recorde2"));
                PlayerPrefs.SetInt("Recorde2", PlayerPrefs.GetInt("Recorde1"));
                PlayerPrefs.SetInt("Recorde1", pontos);
                //PlayerPrefs.SetString("Name1",nome); // Resgistra o nome


                gravado = true;
            }
            else if (pontos > PlayerPrefs.GetInt("Recorde2") && gravado == false) //Salvando o recorde caso ele seja maior que o segundo.
            {
                //Instantiate(Escrita, new Vector2(0, 0), Quaternion.identity);
                PlayerPrefs.SetInt("Recorde4", PlayerPrefs.GetInt("Recorde3"));
                PlayerPrefs.SetInt("Recorde3", PlayerPrefs.GetInt("Recorde2"));
                PlayerPrefs.SetInt("Recorde2", pontos);
                //PlayerPrefs.SetString("Name2",nome);


                gravado = true;
            }
            else if (pontos > PlayerPrefs.GetInt("Recorde3") && gravado == false) //Salvando o recorde caso ele seja maior que o terceiro.
            {
                //Instantiate(Escrita, new Vector2(0, 0), Quaternion.identity);
                PlayerPrefs.SetInt("Recorde4", PlayerPrefs.GetInt("Recorde3"));
                PlayerPrefs.SetInt("Recorde3", pontos);
                //PlayerPrefs.SetString("Name3",nome);


                gravado = true;
            }
            else if (pontos > PlayerPrefs.GetInt("Recorde4") && gravado == false) //Salvando o recorde caso ele seja maior que o quarto. 
            {
                //Instantiate(Escrita, new Vector2(0, 0) , Quaternion.identity);
                PlayerPrefs.SetInt("Recorde4", pontos);
                //PlayerPrefs.SetString("Name4",nome);



            }

            Fim = false;

        }


        if (Fim == false)
        {

            StartCoroutine(hurtCooldown(13f));

            // Apenas irá para a próxima cena depois de tudo acabar

            IEnumerator hurtCooldown(float t)
            {

            yield return new WaitForSeconds(t);

                // Obtém o objeto da cena que possui o AudioClip
                GameObject audioSourceGameObject = GameObject.Find("Musica");

                // Obtém o componente AudioSource do objeto.
                AudioSource source = audioSourceGameObject.GetComponent<AudioSource>();

                source.Play();      // Volta a reproduzir a música

                SceneManager.LoadScene("Score");

            }
        }
        
        

        

    }



}
