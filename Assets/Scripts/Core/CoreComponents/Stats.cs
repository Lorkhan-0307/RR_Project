using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

<<<<<<< Updated upstream
=======
    private Death Death { get => death ?? core.GetCoreComponent(ref death); }
    private Death death;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
            //Die()
            Death.Die();
>>>>>>> Stashed changes
        }
    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += Mathf.Clamp(currentHealth + amount, 0, maxHealth);

    }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
}
