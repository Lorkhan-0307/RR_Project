using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] public float firespeed = 20f;
    [SerializeField] public float damage = 20f;
    [SerializeField] public Rigidbody2D bulletRB;
    [SerializeField] float shutdowntime = 1f;
    private float ShutdownTimer = Mathf.Infinity;
    private PlayerHealth Phealth;

    // Start is called before the first frame update
    void Start()
    {
        ShutdownTimer = 0;
        
        bulletRB.velocity = transform.right * firespeed;
        
    }

    private void Update()
    {
        ShutdownTimer += Time.deltaTime;

        if (shutdowntime < ShutdownTimer)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Phealth = collision.transform.GetComponent<PlayerHealth>();
            Phealth.P_TakeDamage(damage);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag != "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
