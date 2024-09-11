using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate(float newHealth, float delta, float maxHealth);

    public event OnHealthChangedDelegate OnHealthChanged;
    public event OnHealthChangedDelegate OnTakeDamage;
    public event Action OnDead;


    [SerializeField] private float maxHealth = 100;
    private float _health = 100;

    private void Awake()
    {
        _health = maxHealth;
    }

    public void ChangeHealth(float amt)
    {
        if (amt == 0 || _health <= 0) { return; } //personally changed this to <= instead of ==
        _health = Mathf.Clamp( _health + amt, 0, maxHealth);

        if (amt < 0)
        {
            OnTakeDamage?.Invoke(_health, amt, maxHealth);
        }

        OnHealthChanged?.Invoke(_health, amt, maxHealth);
        
        if (_health <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
