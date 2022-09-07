using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject effect;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    private Rigidbody2D rb;
    [SerializeField] float moveDirection = -1f;
    [SerializeField] float moveSpeed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        checkExistCondition();
        StartCoroutine(Fire());
        StartCoroutine(Walk());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (moveDirection * moveSpeed, rb.velocity.y);
    }

    void checkExistCondition()
    {
        // If there is mushroom on the left side and there is a mushroom on the right side, then destroy itself
/*        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 100f, LayerMask.NameToLayer("Mushroom"));
        if (hit)
        {
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 100f, LayerMask.NameToLayer("Mushroom"));
            if (hit2)
            {
                Destroy(gameObject);
            }
        }*/
    }

    IEnumerator Fire()
    {
        float fireInterval = Random.Range(2.0f, 4.0f);
        yield return new WaitForSeconds(fireInterval);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Fire());
    }

    IEnumerator Walk()
    {
        float fireInterval = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(fireInterval);
        moveDirection = -moveDirection;
        Flip();
        StartCoroutine(Walk());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

    }
}
