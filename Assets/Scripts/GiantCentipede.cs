using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantCentipede : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    [SerializeField] float speed = 3f;
    float direction = 1f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GiantCentipede"))
        {
            gameObject.transform.position += new Vector3(0, 3f, 0);
            gameObject.transform.Rotate(0,180f,0f,Space.Self);
            direction = -direction;

        }
        else if(collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.Remove();

        }
    }
}
