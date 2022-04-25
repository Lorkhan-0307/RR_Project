using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] public float maxHealth;
    public float currentHealth;

    public Healthbar Healthbar;

    public event Action HealthZero;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;

        if (gameObject.tag == "Player")
        {
            Healthbar.SetMaxHealth(maxHealth);
        }
        else if(gameObject.tag == "Boss")
        {
        }
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HealthZero?.Invoke();
            Debug.Log("!!Current Health is Zero!!");
        }
        if (gameObject.tag == "Player" || gameObject.tag == "Boss") { Healthbar.SetHealth(currentHealth); }
        Debug.Log("Current health is " + currentHealth);

    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (gameObject.tag == "Player" || gameObject.tag == "Boss")
        {
            Healthbar.SetHealth(currentHealth);
        }
    }
}
