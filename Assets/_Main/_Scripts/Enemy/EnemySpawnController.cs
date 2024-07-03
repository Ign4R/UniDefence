using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private bool canSpawn = true;
    private Coroutine spawnerCoroutine;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] private int enemyLevel = 0;
    
    [SerializeField] EnemyStats[] enemyChainsaw;

    /// enemyLevel
    [SerializeField] GameObject enemyParent;
    [Range(1,5f)][SerializeReference] public float spawnRate = 5; //rango de aparicion, range(limita el numero, y no puede ser menor a 1 ni mayor a 10)

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        Instantiate(enemyPrefab[0], enemyParent.transform);
        spawnerCoroutine = StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(enemyPrefab[enemyLevel],enemyParent.transform);
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
