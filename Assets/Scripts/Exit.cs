using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    [SerializeField] Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("!1");
    }

}
