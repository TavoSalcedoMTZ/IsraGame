using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] PuntosDeSpawn;

    public float tiempoEntreSpawns = 3f; 

    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }

    IEnumerator SpawnEnemigos()
    {
        while (true) 
        {
            int cantidadEnemigos = Random.Range(1, 3); 

            for (int i = 0; i < cantidadEnemigos; i++)
            {
            
                Transform punto = PuntosDeSpawn[Random.Range(0, PuntosDeSpawn.Length)];


                Instantiate(enemyPrefab, punto.position, punto.rotation);
            }

          
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
}
