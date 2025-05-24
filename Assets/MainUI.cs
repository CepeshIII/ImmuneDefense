using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countOfWaveTextMesh;
    [SerializeField] private TextMeshProUGUI countOfAntibodyTextMesh;
    [SerializeField] private TextMeshProUGUI countOfKilledEnemyTextMesh;



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
            countOfWaveTextMesh.text = $"waves {gameStats.maxWaveCount.ToString()}/{gameStats.wavesPassed.ToString()}";
        }

        if (countOfAntibodyTextMesh != null)
        {
            countOfAntibodyTextMesh.text = gameStats.countOfAntibody.ToString();
        }

        if (countOfKilledEnemyTextMesh != null)
        {
            countOfKilledEnemyTextMesh.text = gameStats.countOfKilledEnemy.ToString();
        }
    }
}
