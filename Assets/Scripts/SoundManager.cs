using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]  AudioClip playerHitByBullet;
    [SerializeField]  AudioClip enemyDestroyed;
    [SerializeField] AudioClip drinkHealthRefill;
    [SerializeField] AudioClip hitBuilding;
    AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        
    }

    public void PlayerPlaySound(string clip)
    {
        switch (clip)
        {
            case "playerHitByBullet":
                audioSrc.PlayOneShot(playerHitByBullet, 0.3f);
                break;

            case "enemyDestroyed":
                audioSrc.PlayOneShot(enemyDestroyed, 0.2f);
                break;
            case "drinkHealthRefill":
                audioSrc.PlayOneShot(drinkHealthRefill, 0.4f);
                break;
            case "hitBuilding":
                audioSrc.PlayOneShot(hitBuilding, 0.07f);
                break;


        }
    }
}
