using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countOfWaveTextMesh;
    [SerializeField] private TextMeshProUGUI countOfAntibodyTextMesh;

    [SerializeField] private Scrollbar healthyLine;



    public void Start()
    {
        GameManager.Instance.OnMissEnemy += GameManager_OnMissEnemy;
        GameManager.Instance.OnWaveStart += GameManager_OnWaveStart;
        GameManager.Instance.OnEnemyDied += GameManager_OnEnemyDied;
        GameManager.Instance.OnCellSet += GameManager_OnCellSet;
        

        var stats = GameManager.Instance.GetGameStats();
        UpdateUI(stats);
    }

    private void GameManager_OnCellSet(object sender, System.EventArgs e)
    {
        var stats = GameManager.Instance.GetGameStats();
        UpdateUI(stats);
    }

    private void GameManager_OnWaveStart(object sender, System.EventArgs e)
    {
        var stats = GameManager.Instance.GetGameStats();
        UpdateUI(stats);
    }

    private void GameManager_OnEnemyDied(object sender, System.EventArgs e)
    {
        var stats = GameManager.Instance.GetGameStats();
        UpdateUI(stats);
    }

    private void GameManager_OnMissEnemy(object sender, System.EventArgs e)
    {
        var stats = GameManager.Instance.GetGameStats();
        UpdateUI(stats);
    }


    public void UpdateUI(GameStats gameStats)
    {
        if (countOfWaveTextMesh != null) 
        { 
            countOfWaveTextMesh.text = $"waves {gameStats.wavesPassed.ToString()}/{gameStats.enemyWaves.Count.ToString()}";
        }

        if (countOfAntibodyTextMesh != null)
        {
            countOfAntibodyTextMesh.text = gameStats.countOfAntibody.ToString();
        }

        if (healthyLine != null)
        {
            healthyLine.size =  (10f - gameStats.countOfMissEnemy) / (float)gameStats.missEnemyForLose;
        }
    }
}
