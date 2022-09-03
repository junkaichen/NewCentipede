using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // allow us to access GameManager in other class
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
}
