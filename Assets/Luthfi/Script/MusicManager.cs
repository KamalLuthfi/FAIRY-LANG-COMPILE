using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource audioSource;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName; // Name of the scene
        public AudioClip musicClip; // Music clip to play for that scene
    }

    public SceneMusic[] sceneMusic; // List of scene-specific music

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate MusicManagers
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the music for the current scene
        AudioClip newClip = null;
        foreach (var sceneMusicEntry in sceneMusic)
        {
            if (sceneMusicEntry.sceneName == scene.name)
            {
                newClip = sceneMusicEntry.musicClip;
                break;
            }
        }

        // If there's a new clip and it's different from the current one, play it
        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
