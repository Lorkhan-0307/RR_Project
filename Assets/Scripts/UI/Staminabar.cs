using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    public Slider slider;
    public Text staminatext;
    private float maxstamina;

    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
        staminatext.text = (stamina.ToString() + "/" + stamina.ToString());
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;
        maxstamina = slider.maxValue;
        staminatext.text = (stamina.ToString() + "/" + maxstamina.ToString());
    }

}
