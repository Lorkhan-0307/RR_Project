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

    private void Start()
    {
        block.SetActive(false);
        CMvcam.GetComponent<CinemachineConfiner>().enabled = false;
    }

    public void BossRoomEnter()
    {
        CMvcam.GetComponent<CinemachineConfiner>().enabled = true;
        block.SetActive(true);
    }



    public void Release()
    {
        CMvcam.GetComponent<CinemachineConfiner>().enabled = false;
        block.SetActive(false);
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