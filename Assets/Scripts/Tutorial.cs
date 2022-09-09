using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    [SerializeField] int messageTime = 5;
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] Mushroom mushroomPrefab;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] GameObject ThirdPhaseMushroom;
    [SerializeField] GameObject GreenBottle;
    [SerializeField] GameObject EnenmyPhase;
    int isPlayerGoLeft = 0;
    int isPlayerGoRight = 0;
    bool isStopped = false;
    [SerializeField] Button continueButton;
    Player myPlayer;

    bool canBeStopped = true;
    

    int currentPhase = 0;

    private void Awake()
    {
        myPlayer = FindObjectOfType<Player>();
    }
  


    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 1)
        {
            if (isPlayerGoRight == 2 && isPlayerGoLeft == 2)
            {
                canBeStopped = true;
                tutorialText.text = "Well Done !";
                continueButton.gameObject.SetActive(true);
            }
        }
        if (currentPhase == 2)
        {
            StopPlayer();
            if (myPlayer.sections[0].transform.position.y > -10f)
            {
                tutorialText.text = "Now you are in a higher row !";
                continueButton.gameObject.SetActive(true);
            }
          
        }
        if (currentPhase == 4)
        {
            if (myPlayer.sections.Count == 5)
            {
                tutorialText.text = "Seems like the Green Bottles can help you grow up";
                continueButton.gameObject.SetActive(true);
            }
        }
    }


    public void GoToNextPhase()
    {
        currentPhase++;
        switch (currentPhase)
        {
            case 1:
                tutorialText.text = "Use A D to Change Direction";
                continueButton.gameObject.SetActive(false);
                canBeStopped = true;
                break;

            case 2:
                tutorialText.text = "Try hit the Building";
                mushroomPrefab.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(false);
                canBeStopped = true;
                break;
            case 3:
                tutorialText.text = "There are three green bottles there";
                GreenBottle.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(true);
                canBeStopped = true;
                break;
            case 4:
                tutorialText.text = "Try to get them";
                ThirdPhaseMushroom.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(false);
                canBeStopped = true;
                break;
            case 5:
                tutorialText.text = "Enemies are coming";
                continueButton.gameObject.SetActive(false);
                StopPlayer();
                Vector3 position = myPlayer.sections[2].transform.position;
                position.y += 5;
                Enemy myEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                StartCoroutine(StopEnemy(myEnemy));
               
                
                canBeStopped = true;
                break;


        }
    }

    IEnumerator StopEnemy(Enemy myEnemy)
    {
        yield return new WaitForSeconds(0.5f);
        myEnemy.enabled = false;
    }


    private void ButtonActivate()
    {
        continueButton.gameObject.SetActive(true);
    }

    public void OnMoveLeft()
    {
        if (currentPhase == 1)
        {
            isPlayerGoLeft++;
        }
        if (isStopped)
        {
            ActivatePlayer();
        }
    }

    public void OnMoveRight()
    {
        if (currentPhase == 1)
        {
            isPlayerGoRight++;
        }
        if (isStopped)
        {
            ActivatePlayer();
        }
    }

    private void StopPlayer()
    {
        if (canBeStopped)
        {
            canBeStopped = false;
            isStopped = true;
            for (int i = 0; i < myPlayer.sections.Count; i++)
            {
                myPlayer.sections[i].enabled = false;
            }
        }


    }

    private void ActivatePlayer()
    {
        isStopped = false;
        for (int i = 0; i < myPlayer.sections.Count; i++)
        {
            myPlayer.sections[i].enabled = true;
        }
    }
}
