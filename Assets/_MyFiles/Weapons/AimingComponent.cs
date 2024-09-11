using UnityEngine;

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange = 10f;
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private bool bOverrides = true;
    [SerializeField] private float heightOverride = 1.5f;
    [SerializeField] private float fwdOffSet;


    private Vector3 _debugAimStart;
    private Vector3 _debugAimDir;

    public GameObject GetAimTarget(Transform aimTransform = null) 
    {
        if (!muzzle) 
        {
            //debug
            return null;
        }
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
            aimStart += aimDir * fwdOffSet;
        }

        _debugAimStart = aimStart;
        _debugAimDir = aimDir;

        if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        { 
            return hitInfo.collider.gameObject;
        }

        return null; 
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_debugAimStart, _debugAimStart + _debugAimDir* aimRange);
    }
}
