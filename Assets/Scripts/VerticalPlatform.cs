using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private PlayerInputHandler playerInputHandler;

    private bool FallThroughInput;
    private bool FallThroughInputStop;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()   
    {
        FallThroughInput = playerInputHandler.FallThroughInput;
        FallThroughInputStop = playerInputHandler.FallThroughInputStop;

        if(FallThroughInput)
        {
            effector.rotationalOffset = 180f;
        }

        if(FallThroughInputStop)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
