using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Centipede : MonoBehaviour
{
    // All the centipede body will be stored into the List
    private List<CentipedeSection> sections = new List<CentipedeSection>();
    public CentipedeSection sectionPrefab;

    public Sprite headSprite;
    public Sprite bodySprite;

    public int size = 12;
    public float speed = 1f;
    public LayerMask collisionMask;
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] Mushroom mushroomPrefab;

    float xDirection = 1;
    float yDirection = 0;

    private SpriteRenderer myspriteRenderer;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //myspriteRenderer = GetComponent<SpriteRenderer>();
        //myRigidbody2D = GetComponent<Rigidbody2D>();
        Respawn();
    }

    private void Respawn()
    {
        foreach (CentipedeSection section in sections)
        {
            Destroy(section.gameObject);
        }

        sections.Clear();

        for (int i = 0; i < size; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSection section = Instantiate(sectionPrefab, position, Quaternion.identity);
            section.centipede = this;
            // if in the beginning of the list, it is head, otherwise is body
            section.SpriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            sections.Add(section);
        }

        for (int i = 0; i < sections.Count; i++)
        {
            CentipedeSection section = sections[i];
            section.Ahead = GetSectionAt(i - 1);
            section.Behind = GetSectionAt(i + 1);
        }
    }

    private CentipedeSection GetSectionAt(int index)
    {
        if (index >= 0 && index < sections.Count)
        {
            return sections[index];
        }
        else
        {
            return null;
        }
    }

    // Align the position in a Grid
    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }




    // Update is called once per frame
    void Update()
    {
        //EnemyMovement(xDirection, yDirection);
   

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
        //xDirection = -xDirection;

    }

    public void Remove(CentipedeSection section)
    {
        Vector3 position = GridPosition(section.transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);
        if (section.Ahead != null)
        {
            section.Ahead.Behind = null;
        }
        if (section.Behind != null)
        {
            section.Behind.Ahead = null;
            section.Behind.SpriteRenderer.sprite = headSprite;
            section.Behind.UpdateHeadSection();
        }
        sections.Remove(section);
        Destroy(section.gameObject);
    }

}

