using System;
using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour, IBehaviorInterface, ITeamInterface
{
    [SerializeField] private int teamID = 1;
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int _deadId = Animator.StringToHash("Dead");
    

    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviourGraphAgent;

    public int GetTeamID() 
    {
        return teamID;
    }

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
            _behaviourGraphAgent.BlackboardReference.SetVariableValue<GameObject>("Target", null);
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("checkoutLocation", target.transform.position);
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("hasCheckLocation", true);
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

    public void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }
}
