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
    [Range(1,5f)][SerializeReference] public float spawnRate = 5; //rango de aparicion, range(limita el numero, y no puede ser menor a 1 ni mayor a 10)

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        Instantiate(enemyPrefabs[enemyLevel], enemyParent.transform);
        spawnerCoroutine = StartCoroutine(SpawnNewEnemy());

    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Ensure enemyLevel is within the correct range
            if (enemyLevel >= 0 && enemyLevel <= enemyPrefabs.Length)
            {
                Instantiate(enemyPrefabs[enemyLevel], enemyParent.transform);
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
}
