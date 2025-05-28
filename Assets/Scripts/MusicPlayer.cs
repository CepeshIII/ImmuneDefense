using UnityEngine;
using UnityEngine.Rendering;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public static MusicPlayer Instance;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.Play();

            if (!PlayerPrefs.HasKey("musicVolume")) 
            {
                PlayerPrefs.SetFloat("musicVolume", 0.8f);
            };
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume");

        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }

    public void UpdateVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
