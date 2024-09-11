using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakeDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
    }
    private void StartDeath() 
    {
        Destroy(gameObject);
    }
    private void TookDamage(float newHealth, float delta, float maxHealth) 
    {
        Debug.Log($"I took {delta} amt of damage, health is not {newHealth}/{maxHealth}");
    }
}
