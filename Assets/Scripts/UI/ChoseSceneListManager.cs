using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SceneData
{
    public int number;
    public int id;
}


public class ChoseSceneListManager : MonoBehaviour
{
    [SerializeField] private List<SceneData> scenesData;
    [SerializeField] private GameObject sceneLinePrefab;

    public static ChoseSceneListManager Instance;

    private SceneData chosenData;

    public event EventHandler OnSceneChosen;

    public SceneData ChosenData => chosenData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        foreach (var sceneData in scenesData)
        {
            var sceneLine =  Instantiate(sceneLinePrefab, transform).GetComponent<ChoseSceneLine>();
            sceneLine.SetData(sceneData);
        }
    }

    public void ChoseScene(SceneData sceneData)
    {
        chosenData = sceneData;
        OnSceneChosen?.Invoke(this, new EventArgs());
    }
}
