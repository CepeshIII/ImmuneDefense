using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollSpeedSetting;
    [SerializeField] private Scrollbar volumeSetting;

    private float minScrollSpeed = -0.005f;
    private float maxScrollSpeed = -0.08f;


    private void OnEnable()
    {
        var scrollSpeedValue = PlayerPrefs.GetFloat("scrollSpeed");
        var musicVolume = PlayerPrefs.GetFloat("musicVolume");

        UpdateDragSpeed(scrollSpeedValue);
        UpdateMusicVolume(musicVolume);

        scrollSpeedSetting.value = Mathf.InverseLerp(minScrollSpeed, maxScrollSpeed, scrollSpeedValue);
        volumeSetting.value = musicVolume;
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        volumeSetting.onValueChanged.AddListener((float volume) => { UpdateMusicVolume(volume); });
    }

    public void UpdateScrollSpeed()
    {
        var newValue = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, scrollSpeedSetting.value);
        UpdateDragSpeed(newValue);
    }

    public void UpdateDragSpeed(float newValue)
    {
        if (Camera.main.TryGetComponent<DragCamera2D>(out var dragCamera2D))
        {
            dragCamera2D.dragSpeed = newValue;
        }
        PlayerPrefs.SetFloat("scrollSpeed", newValue);
    }

    public void UpdateMusicVolume(float volume)
    {
        if (MusicPlayer.Instance != null) 
        {
            MusicPlayer.Instance.UpdateVolume(volume);
        }
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
