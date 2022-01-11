using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //버튼 이름 선언
    public string moveAxisName = "Horizontal";
    public string jumpButtonName = "Vertical";

    //input 함수들
    public float move { get; private set; }
    public float jump { get; private set; }

    void Start()
    {
        
    }

    //Update로 Input 읽어들임
    void Update()
    {
        move = Input.GetAxisRaw(moveAxisName);
        jump = Input.GetAxis(jumpButtonName);
    }
}
