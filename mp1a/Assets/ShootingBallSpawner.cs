using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBallSpawner : MonoBehaviour
{
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private GameObject shootingBallTemplate;
    [SerializeField] private Transform shootingController;
    [SerializeField] private Transform attractor;
    [SerializeField] private float orbitGravity = 5f;
    [SerializeField] private float spawnOffset = 0.25f;
    [SerializeField] private AudioSource shootingSound;
    [SerializeField] private ParticleSystem shootingParticle;
    [SerializeField] private SpatialSoundFeedback soundFeedback;

    private bool enabledAction;

    private void OnEnable()
    {
        if (shootAction == null)
        {
            Debug.LogError("Shooting Ball Spawner needs a Shoot Action.", this);
            return;
        }

        shootAction.action.performed += OnShootPerformed;

        if (!shootAction.action.enabled)
        {
            shootAction.action.Enable();
            enabledAction = true;
        }
    }

    private void OnDisable()
    {
        if (shootAction == null)
        {
            return;
        }

        shootAction.action.performed -= OnShootPerformed;

        if (enabledAction)
        {
            shootAction.action.Disable();
            enabledAction = false;
        }
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        ShootBall();
    }

    public void ShootBall()
    {
        if (shootingBallTemplate == null || shootingController == null || attractor == null)
        {
            Debug.LogError("Shooting Ball Spawner needs a ball template, controller, and attractor.", this);
            return;
        }

        if (orbitGravity <= 0f)
        {
            Debug.LogError("Orbit Gravity must be greater than zero.", this);
            return;
        }

        Vector3 controllerDirection = shootingController.forward.normalized;
        Vector3 spawnPosition = shootingController.position + controllerDirection * spawnOffset;
        Vector3 radialOffset = spawnPosition - attractor.position;
        float distance = radialOffset.magnitude;
        if (distance < 0.5f)
        {
            Debug.LogWarning("Move the shooting controller farther from the attractor before launching an orbit.", this);
            return;
        }

        Vector3 radialDirection = radialOffset / distance;
        Vector3 orbitDirection = Vector3.ProjectOnPlane(controllerDirection, radialDirection);
        if (orbitDirection.sqrMagnitude < 0.0001f)
        {
            // A directly inward/outward aim has no tangent. Use the controller's
            // up direction to choose an orbit plane, never the camera's rotation.
            orbitDirection = Vector3.ProjectOnPlane(shootingController.up, radialDirection);
        }
        Vector3 initialVelocity = orbitDirection.normalized * Mathf.Sqrt(orbitGravity / distance);

        GameObject spawnedBall = Instantiate(
            shootingBallTemplate,
            spawnPosition,
            shootingController.rotation);

        ShootingBall movement = spawnedBall.GetComponent<ShootingBall>();
        if (movement == null)
        {
            movement = spawnedBall.AddComponent<ShootingBall>();
        }

        movement.Initialize(attractor, orbitGravity, initialVelocity);
        spawnedBall.SetActive(true);

        if (soundFeedback != null) soundFeedback.PlayCarHorn();

        if (shootingSound != null)
        {
            shootingSound.transform.position = spawnPosition;
            shootingSound.Stop();
            shootingSound.Play();
        }

        if (shootingParticle != null)
        {
            shootingParticle.transform.position = spawnPosition;
            shootingParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            shootingParticle.Emit(30);
        }
    }
}
