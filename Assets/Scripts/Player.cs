using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4.0f;
    private float inputX;
    private float inputY;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        myRigidbody.velocity = new Vector2(inputX * movementSpeed, inputY * movementSpeed);
    }


    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }
}
