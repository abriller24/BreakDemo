using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Speed Boost")]
public class SpeedBoostAbility : Ability
{
    [SerializeField] float boostAmt = 20f;
    [SerializeField] float boostDuration = 3f;

    MovementComponent _movementComponent;

    public override void Init(AbilitiySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _movementComponent = abilitySystemComponent.GetComponent<MovementComponent>();
    }
    protected override void ActivateAbility()
    {
        if (CommitAbility() && _movementComponent != null)
        {
            OwnerASC.StartCoroutine(BoostCoroutine());
        }
    }

    IEnumerator BoostCoroutine()
    {
        _movementComponent.MoveSpeed += boostAmt;
        yield return new WaitForSeconds(boostDuration);
        _movementComponent.MoveSpeed -= boostAmt;
    }
}
