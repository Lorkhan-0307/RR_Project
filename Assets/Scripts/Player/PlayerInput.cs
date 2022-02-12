using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //버튼 이름 선언
    public string moveAxisName = "Horizontal";
    public string jumpButtonName = "Vertical";
    public const string InteractKey = "e";
    public const string rollKey = "space";
    public const int attackKey = 0;
    //input 함수들
    public float move { get; private set; }
    public float jump { get; private set; }
    public bool interact { get; private set; }
    public bool roll { get; private set; }
    public bool attack { get; private set; }

    void Start()
    {
        
    }

    //Update로 Input 읽어들임
    void Update()
    {
        move = Input.GetAxisRaw(moveAxisName);
        jump = Input.GetAxis(jumpButtonName);
        interact = Input.GetKeyDown(InteractKey);
        roll = Input.GetKeyDown(rollKey);
        attack = Input.GetMouseButtonDown(attackKey);
    }
}
