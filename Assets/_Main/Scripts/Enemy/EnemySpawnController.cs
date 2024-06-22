using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private bool canSpawn = true;
    private Coroutine spawnerCoroutine;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject enemyParent;
    [Range(0,20)][SerializeReference]float spawnRate = 20f; //rango de aparicion, range(limita el numero, y no puede ser menor a 1 ni mayor a 10)

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        spawnerCoroutine = StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / spawnRate);
            Instantiate(enemyPrefab[0]);
        }

    }

    public void StopSpawn()
    {
        StopCoroutine(spawnerCoroutine);
    }
}
