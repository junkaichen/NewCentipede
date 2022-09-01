using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Centipede : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    float xDirection = 1;
    float yDirection = 0;

    private SpriteRenderer myspriteRenderer;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myspriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody2D = GetComponent<Rigidbody2D>();   
    }





    // Update is called once per frame
    void Update()
    {
        EnemyMovement(xDirection, yDirection);
   

    }


    private void EnemyMovement(float xDdirection, float yDirection)
    {
        Vector2 movementDirecton = new Vector2 (xDirection, yDirection);
        movementDirecton.Normalize();
        myRigidbody2D.velocity = new Vector2(xDirection * movementSpeed, myRigidbody2D.velocity.y);

        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirecton);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        xDirection = -xDirection;

    }

}

