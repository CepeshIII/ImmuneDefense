using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private float maxWaveTimer = 5f;
    [SerializeField] private float waveTimer = 0;
    [SerializeField] private int countEnemyInWave = 3;
    [SerializeField] private bool spawnOnAwake = false;

    private void Start()
    {
        if (spawnOnAwake)
        {
            OnSpawnTimerEnd();
        }

    }


    private void Update()
    {
        var stats = GameManager.Instance.GetGameStats();
        if (stats.maxWaveCount > stats.wavesPassed)
        {
            waveTimer -= Time.deltaTime;
    
            if(waveTimer <= 0)
            {
                GameManager.Instance.TriggerOnWaveStart();
                OnSpawnTimerEnd();
            }
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
