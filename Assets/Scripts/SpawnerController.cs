using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private float spawnerCooldown;
    public bool canSpawn;
    private void Start()
    {
        canSpawn = true;
    }

    private void Update()
    {
        CheckAnyPlant();
        // Si es de noche y ...
        if (canSpawn)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        Vector2 randomPos = new Vector2(Random.Range(-1, 1), Random.Range(-1f, 1f)).normalized * 10;
        Instantiate(EnemyPrefab, randomPos, new());
        StartCoroutine(SpawnCD());
    }
    public void CheckAnyPlant()
    {
        var anyPlant = GameObject.FindGameObjectWithTag("Plant");
        if (anyPlant == null) canSpawn = false;
    }
    IEnumerator SpawnCD()
    {
        canSpawn = false;
        yield return new WaitForSecondsRealtime(spawnerCooldown);
        canSpawn = true;
    }
}
