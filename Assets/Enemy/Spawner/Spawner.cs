using UnityEngine;

[RequireComponent(typeof(SpawnComponent))]
public class Spawner : Enemy
{
   private SpawnComponent _spawnComponent;
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
         _spawnComponent.StartSpawn();
      }
   }

   protected override void OnDead()
   {
      foreach (var particleSystemSpec in _particleSystemSpec)
      {
         ParticleSystem newVFX = Instantiate(particleSystemSpec.particleSystem, transform.position, Quaternion.identity);
         newVFX.transform.localScale = Vector3.one * particleSystemSpec.size;
      }
   }
}
