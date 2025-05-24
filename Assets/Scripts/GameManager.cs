using System;
using UnityEngine;

public class GameStats
{
    public int countOfKilledEnemy = 0;
    public int countOfMissEnemy = 0;
    public int countOfAntibody = 100;
    public int maxWaveCount = 10;
    public int wavesPassed = 0;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public event EventHandler OnMissEnemy;
    public event EventHandler OnEnemyDied;
    public event EventHandler OnWaveStart;
    public event EventHandler OnCellSet;


    private GameStats stats; 



    public void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
        }

        stats = new GameStats();
    }


    public void TrigerOnMissEnemy()
    {
        stats.countOfMissEnemy++;
        OnMissEnemy?.Invoke(this, new EventArgs());
    }

    public void TrigerOnEnemyDied(int reward)
    {
        stats.countOfKilledEnemy++;
        stats.countOfAntibody += reward;
        OnMissEnemy?.Invoke(this, new EventArgs());
    }

    public void TriggerOnWaveStart()
    {
        stats.wavesPassed++;
        OnWaveStart?.Invoke(this, new EventArgs());
    }

    public void TriggerOnCellSet(int amount)
    {
        stats.countOfAntibody -= amount;
        OnCellSet?.Invoke(this, new EventArgs());
    }

    public GameStats GetGameStats()
    {
        return stats;
    }
}
