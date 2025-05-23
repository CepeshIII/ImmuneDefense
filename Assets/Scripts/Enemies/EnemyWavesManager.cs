using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private float maxWaveTimer = 5f;
    [SerializeField] float waveTimer = 0;
    [SerializeField] int countEnemyInWave = 3;
    [SerializeField] bool spawnOnAwake = false;

    private void Start()
    {
        if (spawnOnAwake)
        {
            OnSpawnTimerEnd();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        waveTimer -= Time.deltaTime;

        if(waveTimer <= 0)
        {
            OnSpawnTimerEnd();
        }
    }

    private void OnSpawnTimerEnd()
    {
        var spawnPoint = GetRandomSpawnPoints();
        if (spawnPoint != null)
        {
            spawnPoint.Spawn(countEnemyInWave, 0.5f);
        }
        waveTimer = maxWaveTimer;
    }

    private SpawnPoint GetRandomSpawnPoints()
    {
        if(spawnPoints != null && spawnPoints.Count > 0)
        {
            var index = Random.Range(0, spawnPoints.Count);
            return spawnPoints[index];
        } 
        return null;
    }
}
