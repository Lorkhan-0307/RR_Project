using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    private Animator anim;

    public void Damage(float amount)
    {
        Debug.Log(amount + "Damage Taken");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
