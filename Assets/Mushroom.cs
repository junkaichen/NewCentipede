using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite wholeMushroom;
    [SerializeField] Sprite mushroomPart_1;
    [SerializeField] Sprite mushroomPart_2;
    [SerializeField] Sprite mushroomPart_3;

    int mushRoomHealth = 4;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mushRoomHealth--;
        if (mushRoomHealth == 3)
        {
            mySpriteRenderer.sprite = mushroomPart_1;
        }
        else if (mushRoomHealth == 2)
        {
            mySpriteRenderer.sprite = mushroomPart_2;
        }
        else if (mushRoomHealth == 1)
        {
            mySpriteRenderer.sprite = mushroomPart_3;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
