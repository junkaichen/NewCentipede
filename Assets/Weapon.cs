using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    int ammoLimit = 1;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (ammoLimit > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ammoLimit--;
        }
        
    }

    public void ReloadAmmo()
    {
        ammoLimit = 1;
    }
}
