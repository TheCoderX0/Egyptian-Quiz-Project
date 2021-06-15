using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    //[SerializeField] private int level; <--- this is used to have it(then info) on the inspector window, let it be string(name) or int(index number) of the scene.

    public void ButtonMoveScene(int level)
    {
        StartCoroutine(LoadLevel(level));

        //SceneManager.LoadScene(level);
        //StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(int level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    //IEnumerator LoadLevel(int levelIndex)
    //{
        //transition.SetTrigger("Start");

        //yield return new WaitForSeconds(transitionTime);

    //SceneManager.LoadScene(levelIndex);
    //}
}
