using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Default Music (Plays at Start)")]
    public AudioClip defaultMusic;

    [Header("Scene-Specific Music Settings")]
    public SceneMusic[] sceneMusicList;

    [Header("Global Settings")]
    [Range(0f, 1f)] public float volume = 1f;
    public bool muteOnStart = false;

    private AudioSource audioSource;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;  // Scene name where this setting applies
        public AudioClip musicClip;
        public bool muteMusic; // Should this scene be silent?
    }

    void Awake()
    {
        // Singleton pattern: Ensures only one instance exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        if (muteOnStart)
            audioSource.mute = true;

        // Start default music
        if (defaultMusic != null)
            PlayMusic(defaultMusic);

        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if there's specific music or mute setting for this scene
        foreach (SceneMusic sm in sceneMusicList)
        {
            if (sm.sceneName == scene.name)
            {
                if (sm.muteMusic)
                {
                    MuteMusic(true);
                }
                else
                {
                    PlayMusic(sm.musicClip);
                    MuteMusic(false);
                }
                return;
            }
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;  // Avoid restarting the same track
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void MuteMusic(bool mute)
    {
        audioSource.mute = mute;
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }
}
