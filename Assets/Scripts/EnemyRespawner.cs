using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private BoxCollider2D spawnArea;
    [SerializeField] Enemy prefab;
    [SerializeField] int amount = 50;

    void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SpawnEnemy();
    }


    private void SpawnEnemy()
    {
        // Bounds Represents an axis aligned bounding box.
        Bounds bounds = spawnArea.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Vector2.zero;
            // Round these position value to whole value, so they will align perfectly to a grid
            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Instantiate(prefab, position, Quaternion.identity);
        }
    }

}
