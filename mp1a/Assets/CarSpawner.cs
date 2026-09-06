using UnityEngine;
using UnityEngine.InputSystem;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private InputActionReference spawnAction;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private AudioSource carDropSound;
    [SerializeField] private ParticleSystem carDropParticle;

    private bool enabledAction;

    private void OnEnable()
    {
        if (spawnAction == null)
        {
            Debug.LogError("Car Spawner needs a Spawn Action.", this);
            return;
        }

        spawnAction.action.performed += OnSpawnPerformed;

        if (!spawnAction.action.enabled)
        {
            spawnAction.action.Enable();
            enabledAction = true;
        }
    }

    private void OnDisable()
    {
        if (spawnAction == null)
        {
            return;
        }

        spawnAction.action.performed -= OnSpawnPerformed;

        if (enabledAction)
        {
            spawnAction.action.Disable();
            enabledAction = false;
        }
    }

    private void OnSpawnPerformed(InputAction.CallbackContext context)
    {
        SpawnCar();
    }

    public void SpawnCar()
    {
        if (carPrefab == null)
        {
            Debug.LogError("Car Spawner needs a Car Prefab.", this);
            return;
        }

        Vector3 randomSpawnPosition = new Vector3(
            Random.Range(-7f, 7f),
            Random.Range(2f, 10f),
            Random.Range(-7f, 7f));

        Instantiate(carPrefab, randomSpawnPosition, Quaternion.identity);

        if (carDropSound != null)
        {
            carDropSound.transform.position = randomSpawnPosition;
            carDropSound.Stop();
            carDropSound.Play();
        }

        if (carDropParticle != null)
        {
            carDropParticle.transform.position = randomSpawnPosition;
            carDropParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            carDropParticle.Emit(30);
        }
    }
}
