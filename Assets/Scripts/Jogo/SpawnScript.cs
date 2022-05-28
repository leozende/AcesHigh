using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Criar, a cada certo espaço de tempo, um avião inimigo

public class SpawnScript : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject Airplane; //Armazena o prefab do Avião

    // Variável para conhecer quando devemos começar a instanciar o avião
    [SerializeField] private float spawnStartTime;

    // Variável para conhecer quão rápido nós devemos criar novos Aviões
    [SerializeField] private float spawnTime;

    [SerializeField] private bool Lateral;

    void Start()
    {
        // Chamar a função 'addEnemy' a cada 'spawnTime' segundos
        InvokeRepeating("addEnemy", spawnStartTime, spawnTime);

    }

    // Nova função para spawnar aviões

    void addEnemy()
    {
         Vector2 spawnPoint;

        if(Lateral == true)
        {
            // Variável para armazenar a posição X do objeto spawn.
            Renderer renderer = GetComponent<Renderer>();
            var y1 = transform.position.y - renderer.bounds.size.y / 2;
            var y2 = transform.position.y + renderer.bounds.size.y / 2;



            // Aleatoriamente escolhe um ponto dentro do objeto spawn
            spawnPoint = new Vector2(transform.position.x, Random.Range(y1, y2));

        }

        else
        {
            // Variável para armazenar a posição X do objeto spawn.
            Renderer renderer = GetComponent<Renderer>();
            var x1 = transform.position.x - renderer.bounds.size.x / 2;
            var x2 = transform.position.x + renderer.bounds.size.x / 2;



            // Aleatoriamente escolhe um ponto dentro do objeto spawn
            spawnPoint = new Vector2(Random.Range(x1, x2), transform.position.y);

        }
        

        // Criar um Avião na posição spawnpoint
        Instantiate(Airplane, spawnPoint, Quaternion.identity);

    }

}
