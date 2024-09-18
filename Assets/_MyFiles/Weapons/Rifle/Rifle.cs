using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private float damage = 5f;
    private AimingComponent _aimingComponent;
    private void Awake()
    {
        _aimingComponent = GetComponent<AimingComponent>();
    }
    public override void Attack()
    {
        GameObject target = _aimingComponent.GetAimResult(Owner.transform);
        if (target)
        {
            HealthComponent targetHealthComponent = target.GetComponent<HealthComponent>();
            targetHealthComponent?.ChangeHealth(-damage);
        }
    }
}
