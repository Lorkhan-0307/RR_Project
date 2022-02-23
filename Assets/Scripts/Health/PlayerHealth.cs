using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    private float currentHealth;
    public Healthbar Healthbar;

    private bool dead;

    public GameOver gameover;

    private void Awake()
    {
        currentHealth = startingHealth;
        Healthbar.SetMaxHealth(startingHealth);
    }

    public void P_TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Healthbar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            //animator.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                //animator.SetTrigger("death");
                dead = true;

                if (GetComponentInParent<PlayerMovement>() != null)
                    GetComponentInParent<PlayerMovement>().enabled = false;
                
                gameover.Setup();


            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            P_TakeDamage(10);
    }
}