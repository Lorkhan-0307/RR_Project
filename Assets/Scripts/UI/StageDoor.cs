using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDoor : Interactable
{
    public override void Interact()
    {
        SceneManager.LoadScene("Room");
    }
}
