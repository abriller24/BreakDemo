using System;
using UnityEngine;
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private HealthComponent _healthComponent;
    private static readonly int DeadId = Animator.StringToHash("Dead");

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _animator = GetComponent<Animator>();
        _healthComponent.OnTakenDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
    }

    private void StartDeath()
    {
        _animator.SetTrigger(DeadId);
        Destroy(gameObject);
    }

    public void DeathAnimationFinished()
    {

    }

    private void TookDamage(float newHealth, float delta, float maxHealth)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newHealth}/{maxHealth}");
    }
}
