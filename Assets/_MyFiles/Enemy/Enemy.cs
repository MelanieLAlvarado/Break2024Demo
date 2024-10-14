using System;
using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour, IBehaviorInterface, ITeamInterface, ISpawnInterface
{
    [SerializeField] private int teamID = 1;
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int _deadId = Animator.StringToHash("Dead");
    

    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviourGraphAgent;

    public GameObject Target 
    {
        get; private set;
    }
    public int GetTeamID() 
    {
        return teamID;
    }

    protected virtual void Awake()
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
            Target = target;
        }
        else
        {
            _behaviourGraphAgent.BlackboardReference.SetVariableValue<GameObject>("Target", null);
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("checkoutLocation", target.transform.position);
            _behaviourGraphAgent.BlackboardReference.SetVariableValue("hasCheckLocation", true);
            Target = null;
        }
    }

    private void StartDeath() 
    {
        _animator.SetTrigger(_deadId);
        //more to be added. (disable collision, disable AI behaviour)
    }
    private void DeathAnimationFinished() 
    {
        OnDead();
        Destroy(gameObject);
    }

    protected virtual void OnDead()
    {
        //override in children
    }

    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator) 
    {
        Debug.Log($"I took {delta} amt of damage, health is not {newHealth}/{maxHealth}");
    }

    public virtual void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }

    public void SpawnedBy(GameObject spawningObj)
    {
        PerceptionComponent spawnerPerceptionComponent = spawningObj.GetComponent<PerceptionComponent>();

        if (!spawnerPerceptionComponent)
        {
            return;
        }    

        GameObject spawnerTarget = spawnerPerceptionComponent.GetCurrentTarget();
        if (!spawnerTarget)
        {
            return;
        }

        Stimuli stimuli = spawnerTarget.GetComponent<Stimuli>();
        if (!stimuli)
        { 
        return ;
        }
        _perceptionComponent.AssignPerceivedStimuli(stimuli);
    }
}
