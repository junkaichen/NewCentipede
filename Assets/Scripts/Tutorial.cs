using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI tutorialTextBot;
    [SerializeField] Mushroom mushroomPrefab;
    [SerializeField] Enemy enemy1;
    [SerializeField] GameObject ThirdPhaseMushroom;
    [SerializeField] GameObject GreenBottle;
    [SerializeField] GameObject EnenmyPhase;
    [SerializeField] GameObject RewardPhase;
    [SerializeField] GameObject EnenmyPhaseTwo;
    [SerializeField] GameObject Arrow1;
    [SerializeField] GameObject Arrow2;
    [SerializeField] GameObject Arrow3;
    [SerializeField] GameObject Arrow4;
    GiantCentipede giantCentipede;
    Vector2 giantCentipedeVelocity;
    int isPlayerGoLeft = 0;
    int isPlayerGoRight = 0;
    bool isStopped = false;
    [SerializeField] Button continueButton;
    [SerializeField] Button continueButton2;
    Player myPlayer;

    bool canBeStopped = true;

    int currentPhase = 0;

    private void Awake()
    {

        myPlayer = FindObjectOfType<Player>();
        giantCentipede = FindObjectOfType<GiantCentipede>();


    }

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
            {
                ActivatePlayer();
            }
        }

        if (currentPhase == 0)
        {
            if (giantCentipede.transform.position.x >= 0)
            {
                StopGiantCentipede();
                continueButton.gameObject.SetActive(true);
            }

        }
        if (currentPhase == 1)
        {
            if (Input.GetKeyDown("a"))
            {
                isPlayerGoLeft++; 
            }
            if (Input.GetKeyDown("d"))
            {
                isPlayerGoRight++;
            }
            StopGiantCentipede();
            Arrow1.SetActive(false);
            if (isPlayerGoRight == 2 && isPlayerGoLeft == 2)
            {
                canBeStopped = true;
                tutorialText.text = "Well Done !";
                continueButton.gameObject.SetActive(true);
            }
        }
        if (currentPhase == 2)
        {
            StopGiantCentipede();
            StopPlayer();
            if (myPlayer.sections[0].transform.position.y > -10f)
            {
                tutorialText.text = "Now you are in a higher row !";
                continueButton.gameObject.SetActive(true);
            }

        }
        if (currentPhase == 3)
        {
            StopGiantCentipede();
        }
        if (currentPhase == 4)
        {
            StopGiantCentipede();
            if (myPlayer.sections.Count == 5)
            {
                tutorialText.text = "Seems like the Green Bottles can help you grow up";
                continueButton.gameObject.SetActive(true);
            }
        }
        if (currentPhase == 5)
        {
            StopGiantCentipede();
        }
        if (currentPhase == 6)
        {
            StopGiantCentipede();
            if (enemy1 == null)
            {
                tutorialText.text = "Congratulation ! Enmey is Destroyed";
                if (myPlayer.GetMushroomCreator() < 1)
                {
                    myPlayer.AddMushroomCreator();
                }

                tutorialText.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(true);
                myPlayer.disableInput();
            }

        }
        if (currentPhase == 7)
        {
            StopGiantCentipede();
        }
        if (currentPhase == 8)
        {
            StopGiantCentipede();
            
        }
        if (currentPhase == 9)
        {
            
            StopGiantCentipede();
            if (!Arrow4.activeInHierarchy)
            {
                if (Input.GetKeyDown("space"))
                {
                    Vector3 position = myPlayer.sections[myPlayer.sections.Count - 1].transform.position;
                    position += new Vector3(-0.1f, 1.6f, 0);
                    Arrow4.transform.position = position;
                    Arrow4.SetActive(true);
                    StartCoroutine(MoveArrowFour(0.5f));
                    tutorialTextBot.text = "Now You can go up !";
                    continueButton2.gameObject.SetActive(true);
                }
            }

        }
        if (currentPhase == 10)
        {
            StopGiantCentipede();
        }
        if (myPlayer.isEnd)
        {
            tutorialText.gameObject.SetActive(false);
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
                tutorialText.text = "Warning! An Enemy is coming for you from the Top, Try to Go up and Eat it";
                continueButton.gameObject.SetActive(true);
                break;
            case 6:
                tutorialText.gameObject.SetActive(false);
                continueButton.gameObject.SetActive(false);
                EnenmyPhase.SetActive(true);
                break;
            case 7:
                tutorialText.text = "You gained an item and 100 pts from its dead Body !";
                Arrow2.SetActive(true);
                Arrow3.SetActive(true);
                continueButton.gameObject.SetActive(true);
                RewardPhase.SetActive(true);
                StartCoroutine(MoveArrowTwo(0.5f));
                StartCoroutine(MoveArrowThree(0.5f));
                break;
            case 8:
                tutorialText.text = "Current row do not have the building, You are stucking at this row.";
                Arrow2.SetActive(false);
                Arrow3.SetActive(false);
                continueButton.gameObject.SetActive(true);
                break;
            case 9:
                myPlayer.enableInput();
                tutorialText.gameObject.SetActive(false);
                tutorialTextBot.text = "Press Space Bar use item to create a building in current row";
                tutorialTextBot.gameObject.SetActive(true);
                Arrow2.SetActive(true);
                continueButton.gameObject.SetActive(false);
                break;
            case 10:
                tutorialTextBot.text = "More Enemies are coming, Destroy them all !";
                Arrow2.SetActive(false);
                Arrow4.SetActive(false);
                continueButton2.gameObject.SetActive(true);
                break;
            case 11:
                tutorialTextBot.text = "Hint: Body hit by Bullet will also create a building in current row";
                giantCentipede.ContinueMoving();
                continueButton2.gameObject.SetActive(false);
                EnenmyPhaseTwo.SetActive(true);
                break;
        }
    }



    IEnumerator MoveArrowTwo(float seconds)
    {
        float moveAmount = 0.6f;
        while (Arrow2)
        {
            Arrow2.transform.position += new Vector3(0, moveAmount, 0);
            moveAmount = -moveAmount;
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator MoveArrowThree(float seconds)
    {
        float moveAmount = 0.6f;
        while (Arrow3)
        {
            Arrow3.transform.position += new Vector3(moveAmount, 0, 0);
            moveAmount = -moveAmount;
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator MoveArrowFour(float seconds)
    {
        float moveAmount = 0.7f;
        while (Arrow4)
        {
            Arrow4.transform.position += new Vector3(0, moveAmount, 0);
            moveAmount = -moveAmount;
            yield return new WaitForSeconds(seconds);
        }
    }





    public void OnCreateMushroom()
    {

        if (currentPhase == 9 && !Arrow4.activeInHierarchy)
        {
            Vector3 position = myPlayer.sections[myPlayer.sections.Count - 1].transform.position;
            position += new Vector3(-0.1f, 1.6f, 0);
            Arrow4.transform.position = position;
            Arrow4.SetActive(true);
            StartCoroutine(MoveArrowFour(0.5f));
            tutorialText.text = "Now You can go up !";
            continueButton.gameObject.SetActive(true);
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
    private void StopGiantCentipede()
    {
        giantCentipede.StopMoving();
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
