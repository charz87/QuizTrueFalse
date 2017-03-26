using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Question> questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private Animator anim;
    



    private void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions;
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "FALSE";
        }
        else
        {
            trueAnswerText.text = "FALSE";
            falseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void UserSelectTrue()
    {
        anim.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        anim.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }


}
