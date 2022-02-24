using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public PlayerHealth phealth;
    public float heal = 40f;
   public void HealthrestoreButton()
    {
        phealth.HealthRestore(heal);
        gameObject.SetActive(false);
    }

    public void CancelButton()
    {
        gameObject.SetActive(false);
    }

}
