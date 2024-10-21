using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void AbilityCooldownStartedDelegate(float cooldownDuration);
    public event AbilityCooldownStartedDelegate OnAbilityCooldownStarted;

    [SerializeField] private float cooldownDuration = 3f;
    [SerializeField] private float manaCost = 10f;
    [SerializeField] private Sprite abilityIcon;
    bool _bIsOnCoolDown;
    protected AbilitySystemComponent OwnerASC 
    {
        get;
        private set;
    }
    public Sprite GetAbilityIcon() { return abilityIcon; }

    public abstract void ActivateAbility();

    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerASC = abilitySystemComponent;
    }
    protected void StartCooldown() 
    {
        OnAbilityCooldownStarted?.Invoke(cooldownDuration);
        OwnerASC.StartCoroutine(CooldownCoroutine());
    }
    private IEnumerator CooldownCoroutine() 
    {
        _bIsOnCoolDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCoolDown = false;
    }
    protected bool CommitAbility()
    {
        if (!OwnerASC)
        {
            return false;
        }
        if (!OwnerASC.TryConsumeMana(manaCost))
        {
            return false; 
        }
        if (_bIsOnCoolDown)
        {
            return false;
        }
        StartCooldown();
        return true;
    }
}
