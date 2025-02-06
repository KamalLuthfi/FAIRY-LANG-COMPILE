using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

[System.Serializable]
public class SceneMusic
{
    public string sceneName;
    public AudioClip musicClip;
    public bool stopMusic;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private SceneMusic[] sceneMusicList;
    [SerializeField] private float fadeDuration = 1f;

    private AudioSource audioSource;
    private string currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentSceneName = SceneManager.GetActiveScene().name;
        CheckSceneMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentSceneName != scene.name)
        {
            currentSceneName = scene.name;
            CheckSceneMusic();
        }
    }

    private void CheckSceneMusic()
    {
        SceneMusic sceneMusic = sceneMusicList.FirstOrDefault(sm => sm.sceneName == currentSceneName);

        if (sceneMusic != null)
        {
            if (sceneMusic.stopMusic)
            {
                StartCoroutine(FadeOut());
            }
            else if (sceneMusic.musicClip != null)
            {
                if (audioSource.clip != sceneMusic.musicClip)
                {
                    StartCoroutine(FadeSwitch(sceneMusic.musicClip));
                }
            }
        }
        // If scene not in list, do nothing - music continues playing
    }

    private IEnumerator FadeSwitch(AudioClip newClip)
    {
        yield return StartCoroutine(FadeOut());
        audioSource.clip = newClip;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator FadeIn()
    {
        float targetVolume = audioSource.volume;
        audioSource.volume = 0;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    // Call these from other scripts to manually control music
    public static void PlayNewTrack(AudioClip clip)
    {
        Instance.StartCoroutine(Instance.FadeSwitch(clip));
    }

    public static void StopMusic()
    {
        Instance.StartCoroutine(Instance.FadeOut());
    }

    public static void ResumeMusic()
    {
        if (!Instance.audioSource.isPlaying)
        {
            Instance.audioSource.Play();
            Instance.StartCoroutine(Instance.FadeIn());
        }
    }
}