using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for the Image component

public class FIBExercise : MonoBehaviour
{
    //Declaration of variables
    int questionNumber;
    public string[] answers;
    public TMP_InputField[] inputFields;
    int score;

    public Color rightColor;
    public Color wrongColor;
    public Color textColorAfterCheck = Color.white; // Default to white


    //Attached to the Check Answer Button
    public void CheckAnswer()
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
                    SetInputFieldColor(inputFields[i], rightColor);
                    isCorrect = true;
                    break;
                }
            }

            if (!isCorrect)
            {
                SetInputFieldColor(inputFields[i], wrongColor);
            }

            // Change the text color to the specified color after checking the answer
            inputFields[i].textComponent.color = textColorAfterCheck;
        }

    }

    //Function to change the Input Field colours
    void SetInputFieldColor(TMP_InputField inputField, Color color)
    {
        Image image = inputField.GetComponentInChildren<Image>();
        if (image != null)
        {
            color.a = 1f; // Ensure the alpha is fully opaque
            image.color = color;
        }
        else
        {

        }
    }

    // Retry Button Function (Navigation/Refresh)
    public void RetryScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
