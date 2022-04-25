using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    

    [SerializeField] GameObject CMvcam;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject block;
    [SerializeField] GameObject bossHealth;

    private void Start()
    {
        block.SetActive(false);
        CMvcam.GetComponent<CinemachineConfiner>().enabled = false;
        bossHealth.SetActive(false);
        boss.SetActive(false);
    }

    public void BossRoomEnter()
    {
        CMvcam.GetComponent<CinemachineConfiner>().enabled = true;
        block.SetActive(true);
        bossHealth.SetActive(true);
        boss.SetActive(true);
        bossHealth.GetComponent<Healthbar>().SetBossName(boss.name);
        bossHealth.GetComponent<Healthbar>().SetMaxHealth(boss.GetComponent<Stats>().maxHealth);
    }



    public void Release()
    {
        CMvcam.GetComponent<CinemachineConfiner>().enabled = false;
        block.SetActive(false);
        bossHealth.SetActive(false);
        boss.SetActive(false);
    }

    private void Update()
    {
        if(boss.GetComponentInChildren<Stats>().currentHealth <= 0)
        {
            Release();
            Debug.Log("BossRoomExit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("BossRoomEnter");
            BossRoomEnter();
        }
    }
}
