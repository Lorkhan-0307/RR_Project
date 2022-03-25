using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Current health is " + currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("!!Current Health is Zero!!");
        }
    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += Mathf.Clamp(currentHealth + amount, 0, maxHealth);

    }
}
