using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    // All the player's section will be stored into the List
    private List<PlayerSection> sections = new List<PlayerSection>();
    public PlayerSection sectionPrefab;

    public Sprite headSprite;
    public Sprite bodySprite;


    [SerializeField] int mushroomCreator = 0;

    public int health = 12;
    public float speed = 1f;
    public LayerMask collisionMask;
    public int turningDirection = 0;
    [SerializeField] Mushroom mushroomPrefab;
    public bool isEnd = false;


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



    public void OnMoveLeft()
    {
        turningDirection = -1;
    }

    public void OnMoveRight()
    {
        turningDirection = 1;
    }

    public void OnCreateMushroom()
    {
        if (mushroomCreator > 0)
        {
            Vector3 position = GridPosition(sections[sections.Count - 1].transform.position);
            Instantiate(mushroomPrefab, position, Quaternion.identity);
            mushroomCreator--;
        }
   
    }

    public void Remove()
    {
        Vector3 position = GridPosition(sections[sections.Count - 1].transform.position);
        Instantiate(mushroomPrefab, position, Quaternion.identity);
        // Remove the last section of Player's body
        PlayerSection currSection = sections[sections.Count - 1];
        sections.Remove(sections[sections.Count - 1]);
        Destroy(currSection.gameObject);
        if (sections.Count == 0)
        {
            isEnd = true;
        }
    }

    public void AddBody()
    {
        Vector2 position;
        if (sections[0].direction.x > 0)
        {
            position = GridPosition(sections[sections.Count - 1].transform.position) + (Vector2.right);
        }
        else
        {
            position = GridPosition(sections[sections.Count - 1].transform.position) + (Vector2.left);
        }
       
        PlayerSection section = Instantiate(sectionPrefab, position, Quaternion.identity);
        section.myPlayer = this;
        section.Ahead = GetSectionAt(sections.Count - 1);
        section.Behind = null;
        sections.Add(section);
        sections[sections.Count - 2].Behind = section;
    }

    public int getBodyLength()
    {
        return sections.Count;
    }

    public void AddMushroomCreator()
    {
        mushroomCreator++;
    }
    public int GetMushroomCreator()
    {
        return mushroomCreator;
    }


}