using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
  
    private int enemyDefeated = 0;


    public void HitEnemyIncrease()
    {
        enemyDefeated++;
    }
    private void Update()
    {
        Debug.Log(enemyDefeated);
    }

    public int GetEnemyDefeated()
    {
        return enemyDefeated;
    }
    public int GetinGameScore()
    {
        return enemyDefeated * 100;
    }

 
}
