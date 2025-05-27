using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSettings", menuName = "ScriptableObjects/SceneSettings", order = 1)]
public class SceneSetting : ScriptableObject
{
    public List<EnemyWave> enemyWaves;
    public int startAntibodyNumber;
}

[Serializable]
public class EnemyWave
{
    public List<EnemyForSpawnData> enemiesForSpawn;
}

[Serializable]
public struct EnemyForSpawnData
{
    public int numberOfEnemy;
    public GameObject enemyPrefab;
}
