using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SpeedBoost")]
public class SpeedBoostAbility : Ability
{
    [SerializeField] float boostAmt = 10f;
    [SerializeField] float boostDuration = 3f;

    MovementComponent _movementComponent;
    public override void Init(AbilitySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _movementComponent = abilitySystemComponent.GetComponent<MovementComponent>();
    }
    protected override void ActivateAbility()
    {
        if (CommitAbility() && _movementComponent != null)
        {
            OwnerASC.StartCoroutine(boostCoroutine());
        }
    }

    private IEnumerator boostCoroutine()
    {
        float newSpeed = _movementComponent.GetMoveSpeed() + boostAmt;
        _movementComponent.SetMoveSpeed(newSpeed);
        yield return new WaitForSeconds(boostDuration);
        _movementComponent.SetMoveSpeed(newSpeed - boostAmt);
    }
}
