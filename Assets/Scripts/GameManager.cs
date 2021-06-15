using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public RoundData[] allRoundData;
    private static List<RoundData> unansweredQuestions;

    private RoundData currentQuestion;
    //private Question currentQuestion;


    // Start is called before the first frame update
    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = allRoundData.ToList<RoundData>();
        }

        GetRandomQuestion();
        Debug.Log(currentQuestion + " is " + currentQuestion);

        DontDestroyOnLoad(gameObject);
    }

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }


    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
