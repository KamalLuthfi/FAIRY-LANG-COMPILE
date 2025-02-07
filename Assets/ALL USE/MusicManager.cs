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
    private bool isMuted = false; // Tracks mute state

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
        public bool muteMusic; // Should this scene be silent?
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Instance.hideFlags = HideFlags.DontUnloadUnusedAsset; // Prevent accidental deletion
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

        if (defaultMusic != null)
            PlayMusic(defaultMusic);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool sceneMuted = false;

        // Check if this scene has specific music or mute setting
        foreach (SceneMusic sm in sceneMusicList)
        {
            if (sm.sceneName == scene.name)
            {
                if (sm.muteMusic)
                {
                    MuteMusic(true);
                    sceneMuted = true;
                }
                else
                {
                    PlayMusic(sm.musicClip);
                    MuteMusic(false);
                }
                return; // Exit loop after finding the scene setting
            }
        }

        // If no specific setting found, resume last played music
        if (!sceneMuted)
        {
            MuteMusic(false);
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
        isMuted = mute;
        audioSource.mute = mute;
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }
}
