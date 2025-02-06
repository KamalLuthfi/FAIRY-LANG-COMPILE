using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }
}
