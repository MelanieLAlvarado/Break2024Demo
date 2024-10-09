using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, ITeamInterface
{
    Rigidbody _rigidbody;

    [SerializeField] float projectileThrowHeight = 3f;
    [SerializeField] float projectileBlowUpDamageRange = 4f;
    [SerializeField] float damage = 20f;

    [SerializeField]ParticleSystem blowUpVFX;
    public int TeamId 
    {
        get;
        private set;
    }

    public GameObject Instigator { get; private set; }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public int GetTeamID() 
    {
        return TeamId;
    }
    public void Launch(Vector3 destination, GameObject instigator) 
    {
        Instigator = instigator;
        ITeamInterface instigatorTeamInterface = instigator.GetComponent<ITeamInterface>();
        TeamId = instigatorTeamInterface.GetTeamID();

        float gravity = Physics.gravity.magnitude;
        float travelHalfTime = Mathf.Sqrt(2 * projectileThrowHeight/gravity);

        float verticalSpeed = gravity * travelHalfTime;

        Vector3 destinationVector = destination - transform.position;
        destination.y = 0f;
        float horizontalSpeed = destinationVector.magnitude / (travelHalfTime * 2f);

        Vector3 launchVelocity = verticalSpeed * Vector3.up + destinationVector.normalized * horizontalSpeed;

        _rigidbody.AddForce(launchVelocity, ForceMode.VelocityChange);
    }
    private bool ShouldBlowUp(GameObject hitObject) 
    {
        //casting to self team interface
        if (((ITeamInterface)this).GetTeamAttitudeTowards(hitObject) == TeamAttitude.Friendly)
        {
            return false;
        }
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ShouldBlowUp(other.gameObject))
        {
            BlowUp();
        }
    }

    private void BlowUp()
    {
        Collider[] collidersInDamageRange = Physics.OverlapSphere(transform.position, projectileBlowUpDamageRange);
        foreach (Collider collider in collidersInDamageRange) 
        {
            if (((ITeamInterface)this).GetTeamAttitudeTowards(collider.gameObject) != TeamAttitude.Enemy)
            {
                continue;
            }
            HealthComponent healthComponent = collider.GetComponent<HealthComponent>();
            if (healthComponent == null)
            {
                continue;
            }

            healthComponent.ChangeHealth(-damage, Instigator);
        }
        if (blowUpVFX) 
        {
            Instantiate(blowUpVFX, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
