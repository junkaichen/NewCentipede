using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSection : MonoBehaviour
{

    ScoreKeeper scoreKeeper;
    public Player myPlayer { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public PlayerSection Ahead { get; set; }
    public PlayerSection Behind { get; set; }
    public bool isOver = false;

    Animator myAnimator;

    public bool isHead => Ahead == null;

    private Vector2 targetPosition;
    // Vector2.right: (1,0)
    // Vector2.up:(0,1)
    public Vector2 direction = Vector2.right + Vector2.up;

    public GameObject bodyDamageEffect;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
        myAnimator = GetComponent<Animator>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    private void Update()
    {
        // Direction changed base on Player input
        if (myPlayer.turningDirection != 0)
        {
            UpdateHeadSectionInCurrRow();
        }
        // Set up the target position for Head
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateHeadSection();
        }
        // End Game
        if (this.transform.position.y > 11.5f)
        {
            myPlayer.isEnd = true;
            
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
        // Body animation, Head animation
        if (!isHead)
        {
            myAnimator.SetBool("isBody", true);
        }
    }


    public void UpdateHeadSectionInCurrRow()
    {
        Vector2 gridPosition = GridPosition(transform.position);
        if (myPlayer.turningDirection < 0)
        {
            if (direction.x > 0)
            {
                direction.x = -direction.x;
                targetPosition.x = gridPosition.x;
                if (Behind != null)
                {
                    Behind.UpdateBodySection();
                }
            }
        }
        else
        {
            if (direction.x < 0)
            {
                direction.x = -direction.x;
                targetPosition.x = gridPosition.x;
                if (Behind != null)
                {
                    Behind.UpdateBodySection();
                }
            }
        }
    
        myPlayer.turningDirection = 0;
    }


    public void UpdateHeadSection()
    {
        Vector2 gridPosition = GridPosition(transform.position);
        // Align position in a grid
        targetPosition = gridPosition;
        // Move forward 1 unit in x direction (Might move back if collide with something)
        targetPosition.x += direction.x;

        // Checks if there is a collider at the targetposition 
        Collider2D hitobjs = Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, myPlayer.collisionMask);
        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, myPlayer.collisionMask))
        {
            if (hitobjs.gameObject.layer == LayerMask.NameToLayer("Barrier"))
            {
                // reverse direction if there is a collider
                direction.x = -direction.x;
                // if overlap something, move backward 1 unit
                targetPosition.x = gridPosition.x;
            }
            else
            {
                // reverse direction if there is a collider
                direction.x = -direction.x;
                // if overlap something, move backward 1 unit
                targetPosition.x = gridPosition.x;
                // go to the new row
                targetPosition.y = gridPosition.y + direction.y;
            }
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
        // Avoid one Bullet destroy more than 1 section
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet") && collision.collider.enabled)
        {         
            if (myPlayer)
            {
                collision.collider.enabled = false;
                myPlayer.Remove();
                Instantiate(bodyDamageEffect, transform.position, Quaternion.identity);
            }            
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("BonusBody"))
        {
            myPlayer.AddBody();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            scoreKeeper.HitEnemyIncrease();
            myPlayer.AddMushroomCreator();
        }
    }






}
