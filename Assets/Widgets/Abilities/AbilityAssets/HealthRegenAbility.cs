using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Health Regen")]
public class HealthRegenAbility : Ability
{
    [SerializeField] float regenAmt = 20f;
    [SerializeField] float regenDuration = 3f;

    HealthComponent _ownerHealthComponent;

    public override void Init(AbilitiySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _ownerHealthComponent = abilitySystemComponent.GetComponent<HealthComponent>();
        _ownerHealthComponent.OnTakenDamage += (_, _, _, _) => BroadcastCanCast();
    }
    public override bool CanCast()
    {
        return base.CanCast() && _ownerHealthComponent.GetHealth() != _ownerHealthComponent.GetMaxHealth()!;
    }

    protected override void ActivateAbility()
    {
        if(!CommitAbility()) return;

        OwnerASC.StartCoroutine(HealthRegenCouroutine());
    }

    IEnumerator HealthRegenCouroutine()
    {
        float counter = 0f;
        float regenRate = regenAmt / regenDuration;
        while(counter < regenDuration)
        {
            counter += Time.deltaTime;
            _ownerHealthComponent.ChangeHealth(regenDuration * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
