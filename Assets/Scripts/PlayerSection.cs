using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSection : MonoBehaviour
{
    public Player myPlayer { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public PlayerSection Ahead { get; set; }
    public PlayerSection Behind { get; set; }

    public bool isHead => Ahead == null;

    private Vector2 targetPosition;
    private Vector2 direction = Vector2.right + Vector2.up;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (myPlayer.isTurning)
        {
            UpdateHeadSectionInCurrRow();
        }
        // Set up the target position for Head
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateHeadSection();
        }

        Vector2 currentPosition = transform.position;
        float speed = myPlayer.speed * Time.deltaTime;
        // Movetoward target position
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, myPlayer.speed * Time.deltaTime);

        //  the direction from our current position to target position
        Vector2 movementDirection = (targetPosition - currentPosition).normalized;
        // Set Rotation base on movement direction
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }


    public void UpdateHeadSectionInCurrRow()
    {
        Vector2 gridPosition = GridPosition(transform.position);
        direction.x = -direction.x;
        targetPosition.x = gridPosition.x;
        if (Behind != null)
        {
            Behind.UpdateBodySection();
        }
        myPlayer.isTurning = false;
    }


    public void UpdateHeadSection()
    {
        Vector2 gridPosition = GridPosition(transform.position);

        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        // Checks if there is a collider at the targetposition 
        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, myPlayer.collisionMask))
        {
            // reverse direction if there is a collider
            direction.x = -direction.x;

            targetPosition.x = gridPosition.x;
            // go to the new row
            targetPosition.y = gridPosition.y + direction.y;
        }

        if (Behind != null)
        {
            Behind.UpdateBodySection();
        }
    }

    private void UpdateBodySection()
    {
        // following the Head
        targetPosition = GridPosition(Ahead.transform.position);
        // Body has same direction as Head
        direction = Ahead.direction;
        // Update all the Body sections
        if (Behind != null)
        {
            Behind.UpdateBodySection();
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            print("aaa");
            /*  collision.collider.enabled = false;*/
            myPlayer.Remove();

        }

    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }






}
