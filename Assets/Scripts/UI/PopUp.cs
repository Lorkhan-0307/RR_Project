using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : Interactable
{
    public GameObject popup;

    public bool isOpen;

    //��ȣ�ۿ� �� �˾�â ���
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
