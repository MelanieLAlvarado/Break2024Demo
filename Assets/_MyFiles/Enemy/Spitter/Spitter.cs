using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform launchTransform;
    public void Shoot() 
    {
        Debug.Log($"Attacking {Target.gameObject.name}");
        if (Target)
        { 
            Projectile newProjectile = Instantiate(projectilePrefab, launchTransform.position, launchTransform.rotation);
            newProjectile.Launch(Target.transform.position, gameObject);
        }
    }
}
