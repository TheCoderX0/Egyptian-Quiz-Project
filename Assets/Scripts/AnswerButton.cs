using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public Text answerText; //Text to display
    private GameController gameController; //reference to game controller

    //private GameManager gameController;

    //private AnswerData answerData; --- //store the answer instance

    private Answer answerData;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        //gameController = FindObjectOfType<GameManager>(); // find the game controller
    }

    //public void Setup(AnswerData data)
    public void Setup(Answer data)//pass in answer data and set up for display
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //handle the button click and send if the answer was correct to the game controller
    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }

}
