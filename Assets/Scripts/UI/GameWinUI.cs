using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] private Button ReturnToMenuButton;
    [SerializeField] private Button ToNextLevelButton;


    public void Start()
    {
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;

        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            ToNextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            ToNextLevelButton.onClick.AddListener(() => { GameManager.Instance.ToNextLevel(); });
        }

        ReturnToMenuButton.onClick.AddListener( () => { GameManager.Instance.ReturnToMenu(); });

        gameObject.SetActive(false);
    }

    private void GameManager_OnGameWin(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);


    }



}
