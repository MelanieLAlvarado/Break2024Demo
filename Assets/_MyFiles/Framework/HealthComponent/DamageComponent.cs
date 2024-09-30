using UnityEngine;

public abstract class DamageComponent : MonoBehaviour
{
    [SerializeField] bool bAttackFriendly;
    [SerializeField] bool bAttackEnemy;
    [SerializeField] bool bAttackNeutral;

    private ITeamInterface _teamInterface;

    public abstract void DoDamage();

    private void Awake()
    {
        _teamInterface = GetComponent<ITeamInterface>();
    }

    protected void ApplyDamage(GameObject target, float damageAmt) 
    {
        Debug.Log($"Damaging {target.gameObject.name}");
        HealthComponent targetHealthComponent = target.GetComponent<HealthComponent>();
        if (targetHealthComponent) 
        {
            targetHealthComponent.ChangeHealth(-damageAmt, gameObject);
        }
    }

    public bool ShouldDamage(GameObject target)
    { 
        TeamAttitude teamAttitude = _teamInterface.GetTeamAttitudeTowards(target);

        if (teamAttitude == TeamAttitude.Enemy && bAttackEnemy)
        {
            return true;
        }
        if (teamAttitude == TeamAttitude.Friendly && bAttackFriendly)
        {
            return true;
        }
        if (teamAttitude == TeamAttitude.Neutral && bAttackNeutral)
        {
            return true;
        }
        return false;
    }
}
