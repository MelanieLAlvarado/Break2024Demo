using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int _deadId = Animator.StringToHash("Dead");
    
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakeDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
    
        _animator = GetComponent<Animator>();
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
