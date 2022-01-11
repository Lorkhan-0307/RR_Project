using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //��ư �̸� ����
    public string moveAxisName = "Horizontal";
    public string jumpButtonName = "Vertical";

    //input �Լ���
    public float move { get; private set; }
    public float jump { get; private set; }

    void Start()
    {
        
    }

    //Update�� Input �о����
    void Update()
    {
        move = Input.GetAxisRaw(moveAxisName);
        jump = Input.GetAxis(jumpButtonName);
    }
}
