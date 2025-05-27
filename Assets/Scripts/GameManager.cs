using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats
{
    public int countOfKilledEnemy = 0;
    public int countOfMissEnemy = 0;
    public int countOfAntibody = 300;
    //public int maxWaveCount = 10;
    public int wavesPassed = 0;

    public int missEnemyForLose = 10;
    public List<EnemyWave> enemyWaves;
}


public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneSetting sceneSetting;

    public static GameManager Instance;

    public event EventHandler OnMissEnemy;
    public event EventHandler OnEnemyDied;
    public event EventHandler OnWaveStart;
    public event EventHandler OnCellSet;
    public event EventHandler OnGameOver;
    public event EventHandler OnGameWin;


    private GameStats stats; 
    


    public void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this;
        }

        stats = new GameStats();

        stats.countOfAntibody = sceneSetting.startAntibodyNumber;
        stats.enemyWaves = sceneSetting.enemyWaves;
    }

    public void Start()
    {
        Unpaused();
    }


    public void TrigerOnMissEnemy()
    {
        stats.countOfMissEnemy++;
        OnMissEnemy?.Invoke(this, new EventArgs());

        if(stats.countOfMissEnemy >= stats.missEnemyForLose)
        {
            GameOver();
        }
    }


    private void GameOver()
    {
        Paused();
        OnGameOver?.Invoke(this, new EventArgs());
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

    public void TriggerOnWavesAreOver()
    {
        Paused();
        OnGameWin?.Invoke(this, new EventArgs());
    }

    public GameStats GetGameStats()
    {
        return stats;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }


    public void Paused()
    {
        Time.timeScale = 0;
    }

    public void Unpaused()
    {
        Time.timeScale = 1;
    }
}
