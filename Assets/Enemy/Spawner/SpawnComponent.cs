using System;
using Unity.Behavior;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnComponent : MonoBehaviour
{
    private static readonly int SpawnId = Animator.StringToHash("Spawn");
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform spawnTransform;
    private Animator _animator;

    private void Awake()
    {
       _animator = GetComponent<Animator>(); 
    }

    public bool StartSpawn()
    {
        if (objectsToSpawn.Length == 0) return false;

        if (_animator != null)
        {
            _animator.SetTrigger(SpawnId);
        }
        else
        {
            Spawn();
        }

        return true;
    }

    public void Spawn()
    {
        int randomPick = Random.Range(0, objectsToSpawn.Length);
        GameObject newSpawn = Instantiate(objectsToSpawn[randomPick], spawnTransform.position, spawnTransform.rotation );
        ISpawnInterface spawnInterface = newSpawn.GetComponent<ISpawnInterface>();
        if (spawnInterface != null)
        {
            spawnInterface.SpawnedBy(gameObject);
        }
    }
}
