using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Only in prefab")]
    [SerializeField] float sceneLoadDelay = 2f;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        //scoreKeeper = FindObjectOfType<ScoreKeeper>(); this is the way done in the lecture but return a NullException, can't found scoreKeeper (but it's in the hierarchy so unclear)
    }

    public void LoadGame()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();  //Following response on the GameDev.TV's forum about the 2D course, people fix the problem above by getting the reference here
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); //Don't work on WebGL or not perfect mobile
    }

    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

}
