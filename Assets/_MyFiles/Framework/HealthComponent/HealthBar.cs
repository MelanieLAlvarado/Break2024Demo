using UnityEngine;

public class HealthBar : ValueGauge
{
    public override void SetOwner(GameObject newOwner) 
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComponent = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComponent)
        {
            ownerHealthComponent.OnHealthChanged += HealthChanged;   //delegate for events
            ownerHealthComponent.OnDead += OwnerDead;//function without the '()' is just the pointer and jumps through the code when the event happens rather than a function call.
        }
    }
    private void OwnerDead() 
    {
        Destroy(gameObject);
    }

    private void HealthChanged(float newHealth, float delta, float maxHealth)
    {
        UpdateValue(newHealth, maxHealth);
    }
}
