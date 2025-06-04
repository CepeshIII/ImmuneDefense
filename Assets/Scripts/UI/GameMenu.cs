using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        GameManager.Instance.Paused();
    }

    private void Start()
    {
        returnToMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ReturnToMenu();
        });

        restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
        });

        closeButton.onClick.AddListener(() => 
        { 
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.Unpaused();
    }
}
