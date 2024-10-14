using System;
using Unity.Behavior;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IBehaviorInterface, ITeamInterface, ISpawnInterface
{
    [SerializeField] private int TeamID = 1;
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int DeadId = Animator.StringToHash("Dead");
    
    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviorGraphAgent;

    public GameObject Target
    { get; private set; }

    public int GetTeamID()
    {
        return TeamID;
    }

    protected virtual void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
        _animator = GetComponent<Animator>();
        _perceptionComponent = GetComponent<PerceptionComponent>();
        _perceptionComponent.OnPerceptionTargetUpdated += HandleTargetUpdate;
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void HandleTargetUpdate(GameObject target, bool bIsSensed)
    {
        Debug.Log($"perception update with {target} and bIsSensed: {bIsSensed}");
        if (bIsSensed)
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("Target", target);
            Target = target;
        }
        else
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue<GameObject>("Target", null);
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("checkoutLocation", target.transform.position);
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("hasCheckLocation", true);
            Target = null;
        }
    }

    private void StartDeath()
    {
        _animator.SetTrigger(DeadId);
    }

    public void DeathAnimationFinished()
    {
        OnDead(); 
        Destroy(gameObject); 
    }

    protected virtual void OnDead()
    {
       //override in child class 
    }
    
    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newHealth}/{maxHealth}");
    }

    public virtual void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }

    public void SpawnedBy(GameObject spanwingObject)
    {
        PerceptionComponent spawnerPerceptionComponent = spanwingObject.GetComponent<PerceptionComponent>();
        if (!spawnerPerceptionComponent)
            return;
        
        GameObject spawnerTarget = spawnerPerceptionComponent.GetCurrentTarget();
        if (!spawnerTarget)
            return;

        Stimuli stimuli = spawnerTarget.GetComponent<Stimuli>();
        if (!stimuli)
            return;

        _perceptionComponent.AssignPerceivedStimuli(stimuli);
    }
}
