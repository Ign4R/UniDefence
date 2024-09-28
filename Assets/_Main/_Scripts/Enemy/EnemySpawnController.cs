using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private bool canSpawn = true;
    private Coroutine spawnerCoroutine;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] private int enemyLevel = 0;

    /// enemyLevel
    [SerializeField] GameObject enemyParent;
    [Range(1,10f)][SerializeReference] public float spawnRate = 10; //rango de aparicion, range(limita el numero, y no puede ser menor a 1 ni mayor a 10)

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        SpawnEnemiesMultipleTimes(4);
        spawnerCoroutine = StartCoroutine(SpawnNewEnemy());

    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Ensure enemyLevel is within the correct range
            if (enemyLevel >= 0 && enemyLevel < enemyPrefabs.Length)
            {
                SpawnEnemiesMultipleTimes(4);


            }
            else
            {
                Debug.LogWarning("Enemy level is out of range!");
            }
        }
    }

    public void ChangeEnemy()
    {
        enemyLevel++;
    }

    public void ReduceSpawnRate()
    {
        if (spawnRate > 1)
        {
            spawnRate--;
        }
        
    }
    public void DestroyEnemies()
    {
        foreach (Transform child in enemyParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(spawnerCoroutine);
    }

    void SpawnEnemiesMultipleTimes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefabs[enemyLevel], enemyParent.transform);
        }
    }
}
