using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{

    [SerializeField] private GameObject[] deathParticles;

    //private ParticleManager ParticleManager { get => particleManager ?? core.GetCoreComponent(ref particleManager); }
    //private ParticleManager particleManager;

    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    public override void Init(Core core)
    {
        base.Init(core);

        Stats.HealthZero += Die;
    }


    public void Die()
    {

        //GameManager에서 EndGame실행되어야만 null 안됨.
        //if (/*playerOnDeath != null && */gameObject.tag == "Player")
        /*{
            Debug.Log("Player Died");
            FindObjectOfType<GameManager>().EndGame();
            //playerOnDeath();
        }*/

        //if (/*playerOnDeath != null && */gameObject.tag == "Enemy")
        /*{
            Debug.Log("Enemy Died");
            //playerOnDeath();
        }*/

        foreach(var particle in deathParticles)
        {
            //ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Stats.HealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.HealthZero -= Die;
    }
}
