using UnityEngine;

[RequireComponent(typeof(SpawnComponent))]
public class Spawner : Enemy
{
    SpawnComponent _spawnComponent;
    [SerializeField] ParticleSystemSpec[] _particleSystemSpec;

    protected override void Awake()
    {
        base.Awake();
        _spawnComponent = GetComponent<SpawnComponent>();
    }

    public override void Attack(GameObject target)
    {

        if (_spawnComponent)
        {
            Debug.Log("Beginning spawn...");
            _spawnComponent.StartSpawn();
        }
    }
    protected override void OnDead()
    {
        foreach (var particleSystemSpec in _particleSystemSpec)
        {
            ParticleSystem newVfx = Instantiate(particleSystemSpec.particleSystem, transform.position, Quaternion.identity);
            newVfx.transform.localScale = Vector3.one * particleSystemSpec.size;
        }
    }
}
