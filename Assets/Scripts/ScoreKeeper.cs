using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int inGameScore = 0;

    public void HitEnemyIncrease()
    {
        inGameScore = inGameScore + 100;
    }

    public int GetinGameScore()
    {
        return inGameScore;
    }
}
