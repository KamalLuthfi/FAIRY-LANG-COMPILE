using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MCQTest : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public Sprite questionImage;
        public string[] options;
        public int correctOption;
    }

    [System.Serializable]
    public class Grading
    {
        public string gradeDescription;
        public float threshold;
    }

    public Image questionImage;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public GameObject resultsCanvas;
    public TextMeshProUGUI resultsText;
    public Button retryButton;

    public Question[] questions;
    public List<Grading> gradings;
    private int currentQuestionIndex;
    private int score;

    void Start()
    {
        retryButton.onClick.AddListener(RetryTest);
        StartTest();
    }

    void StartTest()
    {
        currentQuestionIndex = 0;
        score = 0;
        resultsCanvas.SetActive(false);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            ShowResults();
            return;
        }

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

    }

    void CheckAnswer(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (index == currentQuestion.correctOption)
        {

            score++;
        }
        else
        {

        }
        currentQuestionIndex++;
        Invoke("ShowQuestion", 2); // Show next question after 2 seconds
    }

    void ShowResults()
    {
        resultsCanvas.SetActive(true);
        float percentage = (float)score / questions.Length * 100;
        string grade = GetGrade(percentage);

        resultsText.text = $"Your Score: {score}/{questions.Length}\nGrade: {grade}";
    }

    string GetGrade(float percentage)
    {
        foreach (Grading grading in gradings)
        {
            if (percentage >= grading.threshold)
            {
                return grading.gradeDescription;
            }
        }
        return "Needs Improvement"; // Default grade if no thresholds are met
    }

    void RetryTest()
    {
        StartTest();
    }
}