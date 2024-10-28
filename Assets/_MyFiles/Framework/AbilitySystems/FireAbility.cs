using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Fire")]
public class FireAbility : Ability
{
    [SerializeField] float damageAmount = 50f;
    [SerializeField] float damageDuration = 3f;


    [SerializeField] float scanRadius = 20f;
    [SerializeField] float scanDuration = 1f;

    [SerializeField] Scanner scannerPrefab;

    [SerializeField] GameObject scanVFX;
    [SerializeField] GameObject burnVFX;
    protected override void ActivateAbility()
    {
        if (!CommitAbility())
            { return; }
        Scanner newScanner = Instantiate(scannerPrefab, OwnerASC.gameObject.transform);
        newScanner.OnTargetDetected += TargetDetected;
        Instantiate(scanVFX, newScanner.ScanPivot);
        newScanner.StartScan(scanRadius, scanDuration);
    }

    private void TargetDetected(GameObject newTarget)
    {

        ITeamInterface targetTeamInterface = newTarget.GetComponent<ITeamInterface>();
        if (targetTeamInterface == null)
        {
            return;
        }

        if (targetTeamInterface.GetTeamAttitudeTowards(OwnerASC.gameObject) != TeamAttitude.Enemy)
        {
            return;
        }

        HealthComponent targetHealthComp = newTarget.GetComponent<HealthComponent>();
        if (!targetHealthComp)
        {
            return;
        }

        OwnerASC.StartCoroutine(DamageCoroutine(targetHealthComp));
    }

    private IEnumerator DamageCoroutine(HealthComponent targetHealthComp)
    {
        float counter = 0f;
        float damageRate = damageAmount / damageDuration;

        GameObject newBurnVFX = Instantiate(burnVFX, targetHealthComp.transform);
        while (counter < damageDuration && targetHealthComp != null)
        {

            counter += Time.deltaTime;
            targetHealthComp.ChangeHealth(-damageRate * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }
        Destroy(newBurnVFX);
    }
}
