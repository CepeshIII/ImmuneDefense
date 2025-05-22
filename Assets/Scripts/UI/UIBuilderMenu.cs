using UnityEngine;

public class UIBuilderMenu : MonoBehaviour
{
    [SerializeField] GameObject menuLinePrefab;

    private void Awake()
    {
        DeleteChildren();
    }

    private void Start()
    {
        var cellsData = CellsLoader.LoadCellsData();

        if (cellsData != null) 
        {
            foreach (var cell in cellsData) 
            {
                var menuLine = Instantiate(menuLinePrefab, transform);
                if(menuLine.TryGetComponent<CellsLine>(out var menuLineLogic))
                {
                    menuLineLogic.SetCellData(cell);
                }
            }
        }
    }

    private void DeleteChildren()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            Destroy(transform.GetChild(i).gameObject);
        }


    }
}
