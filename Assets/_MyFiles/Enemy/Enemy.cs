using System;
using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int _deadId = Animator.StringToHash("Dead");
    

    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviourGraphAgent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakeDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
    
        _animator = GetComponent<Animator>();

        _perceptionComponent = GetComponent<PerceptionComponent>();
        _perceptionComponent.OnPerceptionTargetUpdated += HandleTargetUpdate;
        _behaviourGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void HandleTargetUpdate(GameObject target, bool bIsSensed)
    {
        if (bIsSensed)
        {
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("Target", target);
        }
        else
        {
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("Target", null);
        }
    }

    private void StartDeath() 
    {
        _animator.SetTrigger(_deadId);
        //more to be added. (disable collision, disable AI behaviour)
    }
    private void DeathAnimationFinished() 
    {
        Destroy(gameObject);
    }

    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator) 
    {
        Debug.Log($"I took {delta} amt of damage, health is not {newHealth}/{maxHealth}");
    }
}
