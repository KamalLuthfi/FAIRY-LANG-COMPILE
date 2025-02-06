using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MCQExercise : MonoBehaviour
{

    //Declare all of the variables to be used for the GameObjects linking
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public Sprite questionImage;
        public string[] options;
        public int correctOption;
    }

    public Image questionImage;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI feedbackText;

    public Question[] questions;
    private int currentQuestionIndex;


    //Starting the Scene
    void Start()
    {
        currentQuestionIndex = 0;
        ShowQuestion();
    }

    //Function to show the question
    void ShowQuestion()
    {
        //Tracking the number of questions
        if (currentQuestionIndex >= questions.Length)
        {
            feedbackText.text = "Quiz Completed!";
            return;
        }

        //Update the questions
        Question currentQuestion = questions[currentQuestionIndex];
        questionImage.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
            int index = i; // capture index for the button click event
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
        feedbackText.text = "";
    }

    //Function to show the feedback for every question
    void CheckAnswer(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (index == currentQuestion.correctOption)
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Incorrect!";
        }
        currentQuestionIndex++;
        Invoke("ShowQuestion", 2); // Show the next question after 2 seconds
    }
}

