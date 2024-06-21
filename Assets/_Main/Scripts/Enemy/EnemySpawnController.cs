using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [Range(0,20)][SerializeReference]float spawnRate = 20f; //rango de aparicion, range(limita el numero, y no puede ser menor a 1 ni mayor a 10)

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/spawnRate);

            float random = Random.Range(0.0f, 1.0f);

            Instantiate(enemyPrefab[0]); //strong enemy

            //if(random < GameManager.Instance.difficulty * 0.1f) //arranca con 10 % de probabilidad de que salga el enemigo fuerte.
            //{
            //    Instantiate(enemyPrefab[0]); //strong enemy
            //}
            //else
            //{
            //    Instantiate(enemyPrefab[1]);
            //}
        }
    }
}
