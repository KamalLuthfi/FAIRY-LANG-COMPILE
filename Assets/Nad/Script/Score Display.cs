using UnityEngine;
using TMPro;  // This is for TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;  // Reference to the TextMeshPro UI component

    void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.score;  // Update the score
    }
}
