using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBody : MonoBehaviour
{
    public GameObject starEffect;

    void Start()
    {
        Instantiate(starEffect, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(starEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }            
    }

}
