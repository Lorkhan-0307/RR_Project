using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletfire : MonoBehaviour

    
{
    public Transform firePoint;
    public GameObject bulletPrefab;


    void Update()
    {
        if(Input.GetKeyDown("."))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }


}
