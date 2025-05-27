using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseScenePlayButton : MonoBehaviour
{
    [SerializeField] private Button Button;


    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(
            () => 
            {
                if(ChoseSceneListManager.Instance.ChosenData != null)
                SceneManager.LoadScene(ChoseSceneListManager.Instance.ChosenData.id);
            }
            );

        ChoseSceneListManager.Instance.OnSceneChosen += ChoseSceneListManager_OnSceneChosen;
        gameObject.SetActive(false);
    }

    private void ChoseSceneListManager_OnSceneChosen(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

}
