using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button ReturnToMenuButton;
    [SerializeField] private Button RestartGameButton;

    public void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;

        ReturnToMenuButton.onClick.AddListener(() => { GameManager.Instance.ReturnToMenu(); });
        RestartGameButton.onClick.AddListener(() => { GameManager.Instance.RestartGame(); });

        gameObject.SetActive(false);
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

}
