using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    public void AnimationTurnOffFlipTrigger()
    {
        weapon.AnimationTurnOffFlipTrigger();
    }

    public void AnimationTurnOnFlipTrigger()
    {
        weapon.AnimationTurnOnFlipTrigger();
    }


}
