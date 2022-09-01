using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSection : MonoBehaviour
{
    public Centipede centipede { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public CentipedeSection Ahead { get; set; }
    public CentipedeSection Behind { get; set; }
    public bool isHead => Ahead == null;

    private Vector2 targetPosition;
    private Vector2 direction = Vector2.right + Vector2.down;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateHeadSection();
        }

        Vector2 currentPosition = transform.position;
        float speed = centipede.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed);

        Vector2 movementDirection = (targetPosition - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void UpdateHeadSection()
    {
        Vector2 gridPosition = GridPosition(transform.position);

        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        if (Physics2D.OverlapBox(targetPosition, Vector2.zero, 0f, centipede.collisionMask))
        {
            direction.x = -direction.x;

            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;
        }
        
        if (Behind != null)
        {
            Behind.UpdateBodySection();
        }
    }

    private void UpdateBodySection()
    {
        targetPosition = GridPosition(Ahead.transform.position);
        direction = Ahead.direction;

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
}
