using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    public event Action playerOnDeath;
    //public event Action enemyOnDeath;

    //public LayerMask WhatType { get => whatType; set => whatType = value; }
    //[SerializeField] private LayerMask whatType;


    public void Die()
    {
        if (playerOnDeath != null && gameObject.tag == "Player") //How to distinguish between Player & Enemy?
        {
            playerOnDeath();
            Debug.Log("Player Died");
        }

        /*if (enemyOnDeath != null && gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Died");
            enemyOnDeath();
        }*/
    }
}
