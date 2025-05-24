using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CellsLine : MonoBehaviour
{
    [SerializeField] private CellsData data;
    [SerializeField] private TextMeshProUGUI nameMesh;
    [SerializeField] private TextMeshProUGUI priceMesh;
    [SerializeField] private Image image;

    private Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        if (button != null) 
        { 
            button.onClick.AddListener(() =>
            {
                Builder.Instance.UpdateCellForPlace(data);
            });
        }
    }


    public void SetCellData(CellsData newData)
    {
        data = newData;
        UpdateLine();
    }


    public void UpdateLine()
    {
        image.sprite = data.menuSprite;
        nameMesh.text = data.name;
        priceMesh.text = $"Price: {data.price}";
    }


    public void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
