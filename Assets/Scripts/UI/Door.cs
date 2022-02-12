using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public int stageNum;

    private void Start()
    {
        stageNum = -1;
    }
    public override void Interact()
    {
        if (stageNum != -1)
        {
            SceneManager.LoadScene(stageNum);
        }
        else
        {
            Debug.Log("stage not selected");
        }
    }
}
