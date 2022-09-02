using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 4.0f;

    // All the player's section will be stored into the List
    private List<PlayerSection> sections = new List<PlayerSection>();
    public PlayerSection sectionPrefab;

    public Sprite headSprite;
    public Sprite bodySprite;

    public int health = 12;
    public float speed = 1f;
    public LayerMask collisionMask;
    public bool isTurning = false;
    [SerializeField] Mushroom mushroomPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    private void Respawn()
    {
        foreach (PlayerSection section in sections)
        {
            Destroy(section.gameObject);
        }

        sections.Clear();

        // Player Respawn (Include head the body)
        for (int i = 0; i < health; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            PlayerSection section = Instantiate(sectionPrefab, position, Quaternion.identity);
            section.myPlayer = this;
            // Set up Sprite: if in the beginning of the list, it is head, otherwise is body
            section.SpriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            sections.Add(section);
        }

        // Connect all the Section together
        for (int i = 0; i < sections.Count; i++)
        {
            PlayerSection section = sections[i];
            section.Ahead = GetSectionAt(i - 1);
            section.Behind = GetSectionAt(i + 1);
        }
    }


    private PlayerSection GetSectionAt(int index)
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



    public void OnMove()
    {
        isTurning = true;
    }


    // Note:
    // need to implement: Hit by a bullet, get rid of the last section
    public void Remove()
    {
 /*       Vector3 position = GridPosition(sections[sections.Count - 1].transform.position);*/
        /*   Instantiate(mushroomPrefab, position, Quaternion.identity);*/
        /*      PlayerSection currSection = sections[sections.Count - 1];
              sections.Remove(sections[sections.Count-1]);
              Destroy(currSection.gameObject);*/
        print(sections.Count);
    }

    
}