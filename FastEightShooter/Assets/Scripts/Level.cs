using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour{
    private const int mainMenuSceneIndex = 0;
    private const int gameSceneIndex = 1;
    private const int gameOverSceneIndex = 2;
    private const int victorySceneIndex = 3;
    [SerializeField] float delayInSecondsForGameOver = 2f;
    [SerializeField] float delayInSecondsForGameWon = 3f;


    // functions to access
    public static int MainMenuSceneIndex
    {
        get
        {
            return mainMenuSceneIndex;
        }
    }

    public static int GameSceneIndex
    {
        get
        {
            return gameSceneIndex;
        }
    }

    public static int GameOverSceneIndex
    {
        get
        {
            return gameOverSceneIndex;
        }
    }

    public static int VictorySceneIndex
    {
        get
        {
            return victorySceneIndex;
        }
    }

    public void LoadStartMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuSceneIndex);
        Debug.Log("Main Menu successfully loaded.");
    }

    public void LoadGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == MainMenuSceneIndex)
        {
            animController.IsAnimationStarted = true;
            setAnimControllerSceneIndex();
        } else
        {
            SceneManager.LoadScene(1);
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    private static void setAnimControllerSceneIndex()
    {
        animController.SceneIndex = 1;
    }

    public void LoadGameOver()
    {
        Debug.Log("Loading Game Over Menu");
        StartCoroutine(WaitAndLoadGameOver(delayInSecondsForGameOver));
    }

    IEnumerator WaitAndLoadGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

    public void LoadGameWon()
    {
        Debug.Log("Loading Game Won Menu");
        StartCoroutine(WaitAndLoadGameWon(delayInSecondsForGameWon));
    }

    IEnumerator WaitAndLoadGameWon(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
