using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxTriggerDamageComponent : DamageComponent
{
    [SerializeField] private float damage = 10;
    private HashSet<GameObject> _currentOverlappingTargets = new HashSet<GameObject>();
    public override void DoDamage()
    {
        foreach (GameObject target in _currentOverlappingTargets) 
        {
            ApplyDamage(target, damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ShouldDamage(other.gameObject))
        {
            Debug.Log($"other is {other.gameObject.name}");
            _currentOverlappingTargets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("FLAG !");
        _currentOverlappingTargets.Remove(other.gameObject);
    }
}
