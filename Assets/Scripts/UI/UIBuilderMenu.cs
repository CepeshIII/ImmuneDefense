using UnityEngine;

public class UIBuilderMenu : MonoBehaviour
{
    [SerializeField] GameObject menuCardPrefab;
    [SerializeField] GameObject menuCardList;


    private void Awake()
    {
        DeleteChildren();
    }

    private void Start()
    {
        var cellsData = CellsLoader.LoadCellsData();

        if (cellsData != null && menuCardList != null) 
        {
            foreach (var cell in cellsData) 
            {
                var menuLine = Instantiate(menuCardPrefab, menuCardList.transform);
                if(menuLine.TryGetComponent<CellCard>(out var menuLineLogic))
                {
                    menuLineLogic.SetCellData(cell);
                }
            }
        }
    }

    private void DeleteChildren()
    {
        if(menuCardList != null)
        for (int i = 0; i < menuCardList.transform.childCount; i++) 
        {
            Destroy(menuCardList.transform.GetChild(i).gameObject);
        }
    }
}
