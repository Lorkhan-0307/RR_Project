using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State 
    {
        Walking,
        Idle,
        Knockback,
        Attack,
        Dead
    }

    private State currentState;

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                UpdateIdleState();
                break;

            case State.Walking:
                UpdateWalkingState();
                break;

            case State.Knockback:
                UpdateKnockbackState();
                break;

            case State.Attack:
                UpdateAttackState();
                break;

            case State.Dead:
                UpdateDeadState();
                break;
        
        }
    }

    //--IDLE STATE --------------

    private void EnterIdleState()
    {

    }

    private void UpdateIdleState()
    {

    }

    private void ExitIdleState()
    {

    }


    //--WALKING STATE --------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
  
    }

    private void ExitWalkingState()
    {

    }

    //--KNOCKBACK STATE --------------

    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //--ATTACK STATE --------------

    private void EnterAttackState()
    {

    }

    private void UpdateAttackState()
    {

    }

    private void ExitAttackState()
    {

    }

    //--DEAD STATE --------------

    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

}
