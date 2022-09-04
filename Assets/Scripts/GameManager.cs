using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // allow us to access GameManager in other class
    public static GameManager instance;
    bool gameHasEnded = false;

    public void EndGame()
    {
        gameHasEnded = true;
        Debug.Log("End");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
