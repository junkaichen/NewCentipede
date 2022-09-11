using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator hitAnim;
    [SerializeField] float speed = 15.0f;
    private Rigidbody2D myRigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.velocity = -transform.up * speed;
        gameObject.transform.Rotate(0, 0f, 90f, Space.Self);
        hitAnim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            // Avoid one Bullet destroy more than 1 section
            hitAnim.Play("EnemyHit");
            StartCoroutine(DestroyItself());
        }
     
    }
    
    IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
}
