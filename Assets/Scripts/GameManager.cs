using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // allow us to access GameManager in other class
    public static GameManager instance;
    bool gameHasEnded = false;
    Player player;
    EndGameUI endGameUI;
    InGameUI inGameUI;
    private void Awake()
    {
        endGameUI = FindObjectOfType<EndGameUI>();
        player = FindObjectOfType<Player>();
        inGameUI = FindObjectOfType<InGameUI>();
        endGameUI.gameObject.SetActive(false);
   ;
    }

    private void Update()
    {
        if (player.isEnd)
        {
            gameHasEnded = true;
            if (inGameUI)
            {
                inGameUI.gameObject.SetActive(false);
            }
            
            endGameUI.gameObject.SetActive(true);
            endGameUI.showFinalReport();
        }
        
    }

    public void EndGame()
    {
        gameHasEnded = true;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
