using UnityEngine;
using UnityEngine.UI;


public class CellsLine : MonoBehaviour
{
    [SerializeField] private CellsData data;
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
}
