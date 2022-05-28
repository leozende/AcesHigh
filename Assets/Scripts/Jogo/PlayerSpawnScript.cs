using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject Airplane; //Armazena o prefab do Avião

    void Start()
    {
        Instantiate(Airplane, new Vector2(transform.position.x,transform.position.y), Quaternion.identity); //Cria um avião no começo do jogo
    }

}
