using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyWavesManager : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private float maxWaveTimer = 5f;
    [SerializeField] private float waveTimer = 0;
    [SerializeField] private int countEnemyInWave = 3;

    public static EnemyWavesManager Instance;
    public bool waveStarted = false;

    public void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    private void Start()
    {
        waveStarted = false;
    }


    private void Update()
    {
        if (waveStarted) return;

        var stats = GameManager.Instance.GetGameStats();
        if (stats.enemyWaves.Count > stats.wavesPassed)
        {
            waveTimer -= Time.deltaTime;
    
            if(waveTimer <= 0)
            {
                var currentWave = stats.enemyWaves[stats.wavesPassed];
                OnSpawnTimerEnd(currentWave);
                GameManager.Instance.TriggerOnWaveStart();
            }
        }
    }


    private void OnSpawnTimerEnd(EnemyWave enemyWave)
    {
        var spawnPoint = GetRandomSpawnPoints();
        if (spawnPoint != null)
        {
            spawnPoint.SetWaveData(enemyWave, 0.5f);
            waveStarted = true;
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

    public void TriggerOnWaveEnd()
    {
        waveStarted = false;

        var stats = GameManager.Instance.GetGameStats();
        if (stats.enemyWaves.Count == stats.wavesPassed)
        {
            GameManager.Instance.TriggerOnWavesAreOver();
        }

    }
}
