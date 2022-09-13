using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBody : MonoBehaviour
{
    public GameObject starEffect;
    GameObject tempObject;

    void Start()
    {
        tempObject = Instantiate(starEffect, transform.position, Quaternion.identity);
       /* Instantiate(starEffect, transform.position, Quaternion.identity);*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(tempObject);
            Destroy(gameObject);
        }            
    }

}
