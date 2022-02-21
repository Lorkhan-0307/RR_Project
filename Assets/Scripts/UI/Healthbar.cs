using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public Slider slider;
    public Text healthtext;
    private float maxhealth;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthtext.text = (health.ToString()+"/"+health.ToString());

    }


    public void SetHealth(float health)
    {
        slider.value = health;
        maxhealth = slider.maxValue;
        healthtext.text = (health.ToString() + "/" + maxhealth.ToString());
    }

}
