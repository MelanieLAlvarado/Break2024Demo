using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStartedDelegate(float cooldownDuration);
    public event OnAbilityCooldownStartedDelegate OnAbilityCooldownStarted;

    public delegate void OnAbilityCanCastChangedDelegate(bool bCanCast);
    public event OnAbilityCanCastChangedDelegate OnAbilityCanCastChanged;

    [SerializeField] private float cooldownDuration = 3f;
    [SerializeField] private float manaCost = 10f;
    [SerializeField] private Sprite abilityIcon;
    bool _bIsOnCoolDown;

    protected AbilitySystemComponent OwnerASC 
    {
        get;
        private set;
    }
    protected void BroadcastCanCast() { }

    public Sprite GetAbilityIcon() { return abilityIcon; }
    public bool TryActivateAbility() 
    {
        if (!CanCast())
        {
            return false;
        }
        ActivateAbility();
        return true;
    }
    protected abstract void ActivateAbility();
    public virtual bool CanCast() { return !_bIsOnCoolDown && OwnerASC.Mana >= manaCost; }

    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerASC = abilitySystemComponent;
        OwnerASC.onManaUpdated += (mana, delta, maxMana) => BroadcastCanCast(); //if any of these variables are changes... then it broadcasts.
    }
    protected void StartCooldown() 
    {
        OnAbilityCooldownStarted?.Invoke(cooldownDuration);
        OwnerASC.StartCoroutine(CooldownCoroutine());
        BroadcastCanCast();
    }
    private IEnumerator CooldownCoroutine() 
    {
        _bIsOnCoolDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCoolDown = false;
        BroadcastCanCast();
    }
    protected bool CommitAbility()
    {
        if (!OwnerASC)
        {
            return false;
        }
        if (_bIsOnCoolDown)
        {
            return false;
        }
        if (!OwnerASC.TryConsumeMana(manaCost))
        {
            return false;
        }
        StartCooldown();
        return true;
    }
}
