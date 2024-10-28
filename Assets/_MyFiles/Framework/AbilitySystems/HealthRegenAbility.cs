using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Health Regen")]

public class HealthRegenAbility : Ability
{
    [SerializeField] private float regenAmount = 20f;
    [SerializeField] private float regenDuration = 3f;

    HealthComponent _ownerHealthComponent;

    public override void Init(AbilitySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _ownerHealthComponent = abilitySystemComponent.GetComponent<HealthComponent>();
        _ownerHealthComponent.OnHealthChanged += (health, delta, maxHealth, owner) => BroadcastCanCast();
        //if you don't want those variables you can do "(_, _, _, _)" then Unity shouldn't complain...
    }

    public override bool CanCast()
    {
        return base.CanCast() && _ownerHealthComponent.GetHealth() != _ownerHealthComponent.GetMaxHealth();
    }
    protected override void ActivateAbility()
    {
        if (!CommitAbility()) 
        {
            return;
        }

        OwnerASC.StartCoroutine(HealthRegenCoroutine());
    }
    IEnumerator HealthRegenCoroutine() 
    {
        float counter = 0;
        float regenRate = regenAmount / regenDuration;
        
        while (counter < regenDuration)
        {
            counter += Time.deltaTime;
            _ownerHealthComponent.ChangeHealth(regenRate * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

}
