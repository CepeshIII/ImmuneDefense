using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CellCard : MonoBehaviour
{
    [SerializeField] private CellsData data;
    [SerializeField] private TextMeshProUGUI nameMesh;
    [SerializeField] private TextMeshProUGUI priceMesh;
    [SerializeField] private Image image;
    [SerializeField] private GameObject redOutLine;

    private Button button;

    private void OnEnable()
    {
        if(redOutLine != null)
        {
            redOutLine.SetActive(false);
        }
    }

    public void Start()
    {
        button = GetComponent<Button>();
        if (button != null) 
        { 
            button.onClick.AddListener(() =>
            {
                Builder.Instance.UpdateCellForPlace(data);
            });

            Builder.Instance.OnCellDataUpdate += Builder_OnCellDataUpdate;
        }
    }

    private void Builder_OnCellDataUpdate(object sender, Builder.OnCellDataUpdateArgs e)
    {
        if(e.newCellData == data)
        {
            EnableOutline();
        }
        else
        {
            DisableOutline();
        }
    }

    public void DisableOutline()
    {
        if (redOutLine != null)
            redOutLine.SetActive(false);
    }
    public void EnableOutline()
    {
        if (redOutLine != null)
            redOutLine.SetActive(true);
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
        priceMesh.text = $"{data.price}";
    }


    public void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
