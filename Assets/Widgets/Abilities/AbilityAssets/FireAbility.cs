using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Fire Ability")]

public class FireAbility : Ability
{
    [SerializeField] float damageAmount = 50f;
    [SerializeField] float damageDuration = 3f;

    [SerializeField] float damageRadius = 20f;
    [SerializeField] float damageScanDuration = 1f;

    [SerializeField] GameObject scanVFX;
    [SerializeField] GameObject burnVFX;

    [SerializeField] Scanner scannerPrefab;

    protected override void ActivateAbility()
    {
        if(!CommitAbility()) 
            return;

        Scanner newScanner = Instantiate(scannerPrefab, OwnerASC.gameObject.transform);
        newScanner.OnTargetDetected += TargetDetected;
        Instantiate(scanVFX, newScanner.gameObject.transform);
        newScanner.StartScan(damageRadius, damageScanDuration);
    }

    private void TargetDetected(GameObject newTarget)
    {
        ITeamInterface targetInterface = newTarget.GetComponent<ITeamInterface>();
        if (targetInterface == null)
            return;

        if(targetInterface.GetTeamAttitudeTowards(OwnerASC.gameObject) != TeamAttitude.Enemy)
            return;

        HealthComponent targetHealthComp = newTarget.GetComponent<HealthComponent>();
        if (targetHealthComp == null) 
            return;

        OwnerASC.StartCoroutine(DamageCoroutine(targetHealthComp));
    }

    IEnumerator DamageCoroutine(HealthComponent targetHealthComponent)
    {
        float counter = 0;
        float damageRate = damageAmount / damageDuration;
        Instantiate(burnVFX, targetHealthComponent.transform);
        while(counter < damageDuration && targetHealthComponent != null)
        {
            counter += Time.deltaTime;
            targetHealthComponent.ChangeHealth(-damageRate * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
