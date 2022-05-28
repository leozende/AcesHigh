using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneScript : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private int speed; //Velocidade do avião

    [Header("Shoot")]
    [SerializeField] private GameObject bala; //Colocar a bala aqui

    [Header("Animation")]
    [SerializeField] private Animator animator; // Pegar as animações.
    [SerializeField] private Animator Fade; //Animações de transição.

    [Header("2D")]
    [SerializeField] private Rigidbody2D rb; //Controlar o Rigidbody2D

    [Header("Damage")]
    [SerializeField] private int vidas; // Vida do avião disponibilizada para o LifeScript
    [SerializeField] private float HurtForce; // Força de quando colide
    [SerializeField] private int Damage; // Dano levado quando colide


    [Header("Sound")]
    [SerializeField] private AudioSource Staying;

    private LifeScript lfScript; // Para se comunicar com o script

    private State state = State.idle; //Estado do avião
    private enum State { idle, hurt, Start} //Mudança de estado do avião
    

    void Start()
    {

        lfScript = GameObject.Find("Tela").GetComponent<LifeScript>();
        Começo(); // Animação do começo

    }


    void Update()
    {

        // Se o avião não estiver ferido ou no inicio, ele poderá se movimentar.
        if(state != State.hurt && state != State.Start)
        {
            Movimentar();
        }

        Limites();
        DispararBala();

    }


    //Movimento do avião
    void Movimentar()
    {

         // Fazer o avião Subir e descer

        if (Input.GetAxis("Vertical") != 0)

        {

            if (Input.GetAxis("Vertical") > 0)
            {  
                // Rotacionar para cima
                transform.rotation = Quaternion.Euler(0, 0, 15);
                rb.velocity = new Vector2(rb.velocity.x,speed); // Subir

                // Animando a subida
                animator.SetBool("PressUp", true);

            }
            else
            {
                // Rotacionar para cima
                transform.rotation = Quaternion.Euler(0, 0, -15);
                rb.velocity = new Vector2(rb.velocity.x, -speed); // Descer

                //Animando a descida
                animator.SetBool("PressDown", true);
            }

        }
        
        else
        {
            // Voltar rotação ao normal
            transform.rotation = Quaternion.Euler(0, 0, 0);

            // Retira a aceleração do avião, possibilitando ele de parar no ar.
            rb.velocity = new Vector2(0, 0);

            //Retornando para a animação padrão
            animator.SetBool("PressUp", false);
            animator.SetBool("PressDown", false);

        }

        // Fazer o avião ir para frente e para trás

        if (Input.GetAxis("Horizontal") != 0)
        {

            if (Input.GetAxis("Horizontal") > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y); //Ir em frente
            }

            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);//Retroceder
            }

        }
    }

    //Dispara um projétil
    void DispararBala()
    {
        if (Input.GetKeyDown("z"))
        {
            // Cria uma nova bala na posição atual do avião
            Instantiate(bala, transform.position, Quaternion.identity);

            // Atiava a animação do tiro
            animator.SetBool("PressShot", true);
        }

        else if (Input.GetKeyDown("3"))
        {
            // Cria uma nova bala na posição atual do avião
            Instantiate(bala, transform.position, Quaternion.identity);

            // Atiava a animação do tiro
            animator.SetBool("PressShot", true);
        }

        else
        {
            animator.SetBool("PressShot", false);
        }

    }

    //Limitando o avião na cena
    void Limites()
    {

        // Limite da animação inicial
        if(transform.position.y > -0.5 && state == State.Start)
        {
            rb.velocity = new Vector2(0, 0);

            state = State.idle;

            if(vidas > 0)
            {
                Staying.Play();
            }

            

        }


        //Limitando o tamanho da cena horizontalmente
        if (transform.position.x <= -2.45f || transform.position.x >= 2.45f)
        {
            //Criando um limite para o x
            float xPos = Mathf.Clamp(transform.position.x, -2.45f, 2.45f);

            //Convertendo a posição de x
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }

        //Limitando o tamanho da cena verticalmente
        if (transform.position.y <= -1.7f || transform.position.y >= 1.6f)
        {
            //Criando um limite para o y
            float yPos = Mathf.Clamp(transform.position.y, -1.7f, 1.6f);
            //Convertendo a posição de y
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }

    void Começo()
    {
        state = State.Start;
        // Obtém o objeto da cena que possui o AudioClip
        GameObject audioSourceGameObject = GameObject.Find("Musica");

        // Obtém o componente AudioSource do objeto.
        AudioSource source = audioSourceGameObject.GetComponent<AudioSource>();

        source.Stop();      // Pára a reprodução do áudio, inicia do começo quando for reproduzido novamente.

        // Para o avião voar sozinho até o limite.

        rb.velocity = new Vector2(speed * 0.2f, speed * 0.45f);

        Fade.SetBool("Finish", false);

    }
    


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemiesTag")
        {
            
            vidas = vidas - Damage; //Vida perdida por colisão

            lfScript.vidas = vidas; // Entregar informações sobre a vida.

            // Fazendo o avião retroceder quando ele colide com o outro.

            state = State.hurt;

            if(other.gameObject.transform.position.x > transform.position.x)
            {
                // Inimigo a minha direita, fazendo eu ter que retroceder para a esquerda.
                rb.velocity = new Vector3(-HurtForce, rb.velocity.y,0);

            }
            else
            {
                // Inimigo a minha esqueda, fazendo eu ter que retroceder para a direita.
                rb.velocity = new Vector3(HurtForce, rb.velocity.y,0);

            }
            
            if(other.gameObject.transform.position.y > transform.position.y)
            {
                //Inimigo a cima, fazendo eu ter que ir para baixo.
                rb.velocity = new Vector3(rb.velocity.x, -HurtForce,0);

            }
            else
            {
                //Inimigo a baixo, fazendo eu ter que ir para cima.
                rb.velocity = new Vector3(rb.velocity.x, HurtForce,0);

            }

            StartCoroutine(hurtCooldown(0.3f));
            
            // Retornar o estado do avião depois de um tempo

            IEnumerator hurtCooldown(float t)
            {
                    
                yield return new WaitForSeconds(t);
                
                state = State.idle;
                

            }


        }

        if (vidas <= 0)
        {

            Fade.SetBool("Finish", true);
            
            animator.SetBool("Death", true);

            StartCoroutine(hurtCooldown(0.25f));

            IEnumerator hurtCooldown(float t)
            {

                yield return new WaitForSeconds(t);

                
                // Avião é destruido
            Destroy(this.gameObject);

            }

            

            


        }

    }


}
