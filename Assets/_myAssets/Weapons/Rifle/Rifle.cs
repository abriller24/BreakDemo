using UnityEngine;
[RequireComponent(typeof(AimingComponent))]


public class Rifle : Weapon
{
    private AimingComponent _aimingComponent;

    private void Awake()
    {
        _aimingComponent = GetComponent<AimingComponent>();
    }
    public override void Attack()
    {
        GameObject target = _aimingComponent.GetAimTarget(Owner.transform);
        if (target)
            Debug.Log($"damaging {target.name}");
    }
}
