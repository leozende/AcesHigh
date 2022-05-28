using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{

    [Header("Damage and Health")]
    [SerializeField] private int Damage; //Qual o dano que o avião vai receber dos tiros
    [SerializeField] private int Health; //Quanto de vida ela tem
    [SerializeField] private int Score; //Quanto a nave iniga vai dar de pontos
    private PointScript  ptScript; // Para se comunicar com o script

    [Header("Movement")]
    [SerializeField] private float Velocity; //Velocidade do avião
    [SerializeField] private float Surgir; //De onde o avião vem... (De baixo > 0, de cima < 0 e do meio = 0
    [SerializeField] private float Limit;

    [Header("Animation and Body")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    private void Start()
    {
        ptScript = GameObject.Find("Tela").GetComponent<PointScript>();

    }


    private void Update()
    { 

        Movement();

    }


    // Movimento do avião
    private void Movement()
    {
        // Se o avião não surgir do meio
        if(Surgir != 0)
        {
            
            
            // Vendo se o avião vem de cima ou de baixo
            if(Surgir > 0)
            {
                // Se o avião não acertar o limite, ele vai para a direita.

                if (transform.position.y < -(Surgir))
                {

                    rb.velocity = new Vector2(Velocity, Velocity * Surgir);
                }

                // Quando ele acert o limite, o avião vai para a esquerda
                else
                {

                    rb.velocity = new Vector2(-Velocity, 0);

                    animator.SetBool("Limit", true);
                }

            }

            else
            {
                // Se o avião não acertar o limite, ele vai para a direita.

                if (transform.position.y > - (Surgir))
                {
            
                    rb.velocity = new Vector2(Velocity, Velocity * Surgir);
                }

                // Quando ele acert o limite, o avião vai para a esquerda
                else
                {

                    rb.velocity = new Vector2(- Velocity, 0);
                
                    animator.SetBool("Limit", true);
                }
            }
            
            

        }
        else
        {

            rb.velocity = new Vector2(-Velocity, rb.velocity.y);

        }


    }


    // Quando uma bala acerta diminui a vida e, se ela zerar, o avião é destruido
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BulletTag"))
        {
            Health -= Damage;

            if(Health <= 0)
            {
                animator.SetBool("Death", true);

                StartCoroutine(hurtCooldown(0.45f));

                IEnumerator hurtCooldown(float t)
                {

                    yield return new WaitForSeconds(t);


                    // Avião é destruido
                    Destroy(this.gameObject);

                }


                ptScript.pontos += Score; // Entregar os pontos para o script

            }

        }


    }

    private void OnBecameInvisible()
    {
        // Destroi a nave assim que sai da tela
        Destroy(gameObject);
    }





}
