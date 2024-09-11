using UnityEngine;

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange;
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private bool bOverrides = true;
    [SerializeField] private float heightOverride = 1.5f;
    [SerializeField] private float fwdOffset = .5f;

    Vector3 _debugAimStart;
    Vector3 _debugAimdir;
    public GameObject GetAimTarget(Transform aimTransform = null)
    {
        Vector3 aimStart = muzzle.position;
        Vector3 aimDir = muzzle.forward;
        if (aimTransform)
        {
            aimStart = aimTransform.position;
            aimDir = aimTransform.forward;
        }

        if (bOverrides)
        {
            aimStart.y = heightOverride;
            aimStart += aimDir * fwdOffset;
        }

        _debugAimStart = aimStart;
        _debugAimdir = aimDir;

        if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_debugAimStart, _debugAimStart + _debugAimdir * aimRange);
    }
}
