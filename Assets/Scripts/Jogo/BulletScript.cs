using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //Velocidade da bala
    [SerializeField] private float speed = 5;

    void Start()
    {

        //Faz a bala se mover para a frente (Eixo x)

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed, 0);
    }

    //Quando a bala sai da tela
    private void OnBecameInvisible()
    {
        // Destroi a bala quando já está fora da tela
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "EnemiesTag")
        {

            Destroy(this.gameObject);

        }


    }

}
