using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI questionDisplayText;
    public TextMeshProUGUI scoreDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;


    private GameManager dataController;
    private RoundData currentRoundData;
    private Question[] questionPool;

    private bool isRoundActive;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<GameManager>();//we can use find because we start with the Persistent scene which has a data controller
        SetUpRound();
    }

    public void SetUpRound()
    {
        //Once dataController is loaded initialize game variables with the data found
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;

        //initialize remaining game data and show first question
        playerScore = 0;
        questionIndex = 0;

        ShowPlayerScore();
        ShowQuestion();
        isRoundActive = true;
    }

    private void ShowPlayerScore()
    {
        scoreDisplayText.text = "Karma: " + playerScore.ToString();
    }

    private void ShowQuestion()
    {   //Remove old answers get current question and display the text
        RemoveAnswerButtons();
        Question questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        //Get all answers for the question, create new buttons for each and add them to the answerButtonParent object(AnswerPanel) 
        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            //we get a reference to the answer button then use its attatched script to set the answer
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();//Give the gameobject an answer button component
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {//if answer buttons exists remove them , remove the game object and add it to the available object pool
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        //Increase player score if answer is correct and update the display
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Karma: " + playerScore.ToString();
        }

        //If we have more questions show the next question otherwise end the round
        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }

    }

    public void EndRound()
    {   // set the round over and turn off question display then turn on round end display panel
        isRoundActive = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        if (playerScore >= 50)
        {
            SceneManager.LoadScene(2);
        }else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
