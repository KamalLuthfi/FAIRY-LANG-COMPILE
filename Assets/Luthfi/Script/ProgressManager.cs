using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    public bool[] levelsCompleted;
    public string startTrackingFromScene = "SpecificSceneName"; // Replace with your scene name

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadProgress();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if progress tracking should start
        if (scene.name == startTrackingFromScene || progressTrackingHasStarted)
        {
            progressTrackingHasStarted = true;
            Debug.Log("Progress tracking is now active.");
        }
    }

    private bool progressTrackingHasStarted = false;

    public void MarkLevelComplete(int levelIndex)
    {
        if (!progressTrackingHasStarted) return; // Do nothing if tracking hasn't started

        levelsCompleted[levelIndex] = true;
        SaveProgress();
    }

    public void SaveProgress()
    {
        for (int i = 0; i < levelsCompleted.Length; i++)
        {
            PlayerPrefs.SetInt("Level_" + i, levelsCompleted[i] ? 1 : 0);
        }
    }

    public void LoadProgress()
    {
        for (int i = 0; i < levelsCompleted.Length; i++)
        {
            levelsCompleted[i] = PlayerPrefs.GetInt("Level_" + i, 0) == 1;
        }
    }
}
