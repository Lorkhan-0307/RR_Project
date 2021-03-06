using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}
