using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    //public event Action playerOnDeath;
    //public event Action enemyOnDeath;


    public void Die()
    {
        //GameManager에서 EndGame실행되어야만 null 안됨.
        if (/*playerOnDeath != null && */gameObject.tag == "Player")
        {
            Debug.Log("Player Died");
            FindObjectOfType<GameManager>().EndGame();
            //playerOnDeath();
        }

        if (/*playerOnDeath != null && */gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Died");
            //playerOnDeath();
        }

    }
}
