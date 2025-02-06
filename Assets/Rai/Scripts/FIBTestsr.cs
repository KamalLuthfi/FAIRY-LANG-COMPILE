using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for the Image component

public class FIBTest : MonoBehaviour
{
    int questionNumber;
    public string[] answers;
    public TMP_InputField[] inputFields;
    public GameObject ResultCanvas;
    public TextMeshProUGUI scoreText; // Text component to display the score
    int score;


    public void SubmitAnswer()
    {
        score = 0;

        for (int i = 0; i < answers.Length; i++)
        {
            // Function to separate multiple answer options
            string[] possibleAnswers = answers[i].Split('/');
            bool isCorrect = false;

            foreach (string possibleAnswer in possibleAnswers)
            {
                if (possibleAnswer.Trim().Equals(inputFields[i].text.Trim(), System.StringComparison.OrdinalIgnoreCase))
                {
                    score++;
                    isCorrect = true;
                    ResultCanvas.SetActive(true);
                    if (scoreText != null)
                    {
                        scoreText.text = "Score: " + score.ToString() + " / 5";
                    }
                    break;
                }

                else
                {
                    ResultCanvas.SetActive(true);
                    if (scoreText != null)
                    {
                        scoreText.text = "Score: " + score.ToString() + " / 5";
                    }
                    break;

                }
            }


        }

    }

    // Navigation Function
    public void RetryScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
