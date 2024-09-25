using UnityEngine;

public class AwareRange : Sense
{
    [SerializeField] private float awareRange = 1f;
    protected override void OnDrawDebug() 
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up, awareRange);
    }
}
