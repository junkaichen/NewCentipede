using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    private Rigidbody2D myRigidBody2D;
    Weapon myWeapon;
    // Start is called before the first frame update
    void Start()
    {
        myWeapon = FindObjectOfType<Weapon>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = transform.up * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        myWeapon.ReloadAmmo();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
