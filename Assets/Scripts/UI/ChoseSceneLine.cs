using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseSceneLine : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMesh;

    private SceneData sceneData;

    internal void SetData(SceneData sceneData)
    {
        textMesh.text = "Level " + sceneData.number;
        this.sceneData = sceneData;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(
            () => 
            {
                ChoseSceneListManager.Instance.ChoseScene(sceneData);
            }
            );

    }

    void Update()
    {
        
    }
}
