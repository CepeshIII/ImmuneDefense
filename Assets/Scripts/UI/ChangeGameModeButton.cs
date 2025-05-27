using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameModeButton : MonoBehaviour
{
    private GameMode gameMode;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMesh;

    public enum GameMode
    {
        Game,
        Build
    }

    private void Start()
    {
        button.onClick.AddListener(() => 
        {
            ChangeMode();
        });

        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnGameWin += GameManager_OnGameOver;


        gameMode = GameMode.Game;
        ApplyChange();

        Builder.Instance.gameObject.SetActive(false);
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    public void ChangeMode()
    {
        if (gameMode == GameMode.Game)
        {
            gameMode = GameMode.Build;
        }
        else
        {
            gameMode = GameMode.Game;
        }

        ApplyChange();
    }


    public void ApplyChange()
    {
        if(gameMode == GameMode.Game)
        {
            textMesh.text = "To BuildMode";
            Builder.Instance.SetActive(false);
        }
        else
        {
            textMesh.text = "To GameMode";
            Builder.Instance.SetActive(true);
        }
    }
}
