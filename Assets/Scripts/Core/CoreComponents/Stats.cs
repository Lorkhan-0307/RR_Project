using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] private float maxHealth;
    public float currentHealth;

    public Healthbar Healthbar;


    private Death Death { get => death ?? core.GetCoreComponent(ref death); }
    private Death death;

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
            //core.transform.parent.name
            Healthbar.SetBossName(core.transform.parent.name);
            Healthbar.SetMaxHealth(maxHealth);
        }
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("!!Current Health is Zero!!");
            Death.Die();
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
