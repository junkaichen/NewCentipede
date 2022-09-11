using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnemyDefeatedScore;
    [SerializeField] TextMeshProUGUI BonusBodyScore;
    [SerializeField] TextMeshProUGUI totalScore;

    private ScoreKeeper scoreKeeper;
    private Player player;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        player = FindObjectOfType<Player>();
    }

    public void showFinalReport()
    {
        int total = scoreKeeper.GetEnemyDefeated() * 100 + player.getBodyLength() * 50;
        EnemyDefeatedScore.text = "Enemy Defeated: " + scoreKeeper.GetEnemyDefeated().ToString() + " x 100";
        BonusBodyScore.text = "Body Bonus: " + player.getBodyLength().ToString() + "x 50";
        totalScore.text = "Total Score: " + total.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
