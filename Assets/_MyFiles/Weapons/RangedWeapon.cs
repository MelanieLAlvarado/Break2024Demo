using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class RangedWeapon : Weapon
{
    [SerializeField] private ParticleSystem bulletVFX;
    [SerializeField] private float damage = 5f;
    private AimingComponent _aimingComponent;
    private void Awake()
    {
        _aimingComponent = GetComponent<AimingComponent>();
    }
    public override void Attack()
    {
        AimResult aimResult = _aimingComponent.GetAimResult();
        if (aimResult.target)
        {
            HealthComponent targetHealthComponent = aimResult.target.GetComponent<HealthComponent>();
            targetHealthComponent?.ChangeHealth(-damage);
        }

        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);

        bulletVFX.transform.position = aimResult.aimStart;
        bulletVFX.transform.forward = aimResult.aimDir;
    }
}
