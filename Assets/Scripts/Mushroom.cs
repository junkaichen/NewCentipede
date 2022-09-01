using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite mushroomPart_1;
    [SerializeField] Sprite mushroomPart_2;
    [SerializeField] Sprite mushroomPart_3;

    int mushRoomHealth = 4;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
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
}
