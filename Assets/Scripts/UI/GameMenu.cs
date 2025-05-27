using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button closeButton;


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
            GameManager.Instance.Unpaused();
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
}
