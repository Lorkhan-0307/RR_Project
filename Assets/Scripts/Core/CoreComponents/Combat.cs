using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool isKnockbackActive;
    private float knockbackStartTime;

    [SerializeField]
    private float maxKnockbackTime = 0.2f;

    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Stats stats;
    private CollisionSenses collisionSenses;
    private Movement movement;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }
    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + amount + "Damaged!");
        Stats.DecreaseHealth(amount);
    }

    void IKnockbackable.Knockback(Vector2 angle, float strength, int direction)
    {
        Movement.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if(isKnockbackActive && Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground || Time.time >= knockbackStartTime + maxKnockbackTime)
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}

