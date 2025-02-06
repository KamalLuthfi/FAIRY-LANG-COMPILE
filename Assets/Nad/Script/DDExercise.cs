using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DDExercise : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public GameObject[] questions;
    public string[] questionTexts; // Array to hold the questions
    private int currentQuestionIndex = 0;
    public TextMeshProUGUI feedbackText;


    void Start()
    {
        ShowQuestion();
    }

    public void ShowQuestion()
    {
        // Ensure the questionTexts array is not empty and the index is valid
        if (questionTexts.Length > 0 && currentQuestionIndex < questionTexts.Length)
        {
            questionText.text = questionTexts[currentQuestionIndex];
        }

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(i == currentQuestionIndex);
            feedbackText.text = "";
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            questionText.text = "You've completed all questions!";
        }
    }
}