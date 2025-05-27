using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollSpeedSetting;
    private float minScrollSpeed = -0.08f;
    private float maxScrollSpeed = -0.005f ;


    private void OnEnable()
    {
        var value = PlayerPrefs.GetFloat("scrollSpeed");
        if (Camera.main.TryGetComponent<DragCamera2D>(out var dragCamera2D))
        {
            dragCamera2D.dragSpeed = value;
        }
        scrollSpeedSetting.value = Mathf.InverseLerp(minScrollSpeed, maxScrollSpeed, value);

    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void UpdateScrollSpeed()
    {
        var newValue = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, scrollSpeedSetting.value);
        PlayerPrefs.SetFloat("scrollSpeed", newValue);
        if(Camera.main.TryGetComponent<DragCamera2D>(out var dragCamera2D))
        {
            dragCamera2D.touchDragSpeed = newValue;
        }

    }
}
