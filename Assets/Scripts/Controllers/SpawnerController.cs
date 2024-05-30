using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour, IOnStartGame
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private float spawnerCooldown;
    public bool canSpawn;
    private DayCicle _currentCycle;
    private void Start()
    {

    }
    private void Update()
    {
        CheckAnyPlant();
        // Si es de noche y ...
        if (canSpawn&&_currentCycle==DayCicle.Night)
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

    private void CallSpawn(DayCicle dayCicle)
    {
        _currentCycle = dayCicle;
        if (DayCicle.Night == dayCicle)
        {
            canSpawn = true;
            if (GameManager.Instance.RealTime.Days%2==0&&GameManager.Instance.RealTime.Days!=0)
            {
                if(spawnerCooldown>1.5f)
                spawnerCooldown -= 1;
            }
        }
        else
        {
            foreach (var enemy in FindObjectsOfType<MonoBehaviour>(true).OfType<Enemy>().ToArray())
                enemy.ChangeState(enemy.leaving);
            canSpawn = false;
        }
    }

    public void OnStartGame()
    {
        GameManager.Instance.OnChangeDay += CallSpawn;
    }
}
