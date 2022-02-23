using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float NormalAttackShakeAmount = 0.3f;
    public float ChargeAttackShakeAmount = 0.4f;
    public float ParryAttackShakeAmount = 0.6f;
    

    public float NormalAttackShakeTime = 0.1f;
    public float ChargeAttackShakeTime = 0.1f;
    public float ParryAttackShakeTime = 0.1f;

    public float ShakeAmount;
    public float ShakeTime;

    Vector2 initialPosition;
    public Transform target;

    public void NormalAttackShake()
    {
        ShakeTime = NormalAttackShakeTime;
        ShakeAmount = NormalAttackShakeAmount;
    }

    public void ChargeAttackShake()
    {
        ShakeTime = ChargeAttackShakeTime;
        ShakeAmount = ChargeAttackShakeAmount;
    }

    public void ParryAttackShake()
    {
        ShakeTime = ParryAttackShakeTime;
        ShakeAmount = ParryAttackShakeAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        initialPosition = target.transform.position;
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitCircle * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            //transform.position = initialPosition;
        }

        
    }
}
