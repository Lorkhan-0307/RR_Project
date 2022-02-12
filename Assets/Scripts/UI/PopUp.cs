using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : Interactable
{
    public GameObject popup;

    public bool isOpen;

    //상호작용 시 팝업창 기능
    public override void Interact()
    {
        if (isOpen)
        {
            popup.SetActive(false);
        }
        else
        {
            popup.SetActive(true);
        }
        isOpen = !isOpen;
    }
}
