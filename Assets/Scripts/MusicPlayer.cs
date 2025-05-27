using UnityEngine;

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
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
    }
}
