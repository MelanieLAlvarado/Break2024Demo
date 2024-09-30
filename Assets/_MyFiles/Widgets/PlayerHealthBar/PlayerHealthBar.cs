using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : Widget
{
    [SerializeField] private Image HealthBarImage;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComp = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComp)
        {
            ownerHealthComp.OnHealthChanged += UpdateHealth;
            UpdateHealth();
        }
    }

    private void UpdateHealth() 
    {
        
    }
}
