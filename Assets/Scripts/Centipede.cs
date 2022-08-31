using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Centipede : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float playerMovementSpeed = 4.0f;
    int xDirection = 1;

    private SpriteRenderer spriteRenderer;
  

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }





    // Update is called once per frame
    void Update()
    {
        EnemyMovement(xDirection);
   

    }


    private void EnemyMovement(int direction)
    {
        Vector3 directionTranslate = new Vector3();
 
        directionTranslate.x = Time.deltaTime * movementSpeed * direction;
        transform.Translate(directionTranslate);
        

  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        xDirection = -xDirection;
        transform.Translate(0, -1, 0);
        
    }


}

