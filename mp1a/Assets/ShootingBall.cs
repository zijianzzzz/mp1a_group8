using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    private Transform attractor;
    private float gravity;

    public void Initialize(Transform newAttractor, float newGravity, Vector3 initialVelocity)
    {
        attractor = newAttractor;
        gravity = newGravity;
        velocity = initialVelocity;
        Destroy(gameObject, 60f);
    }

    private void Update()
    {
        if (attractor == null || Time.deltaTime <= 0f)
        {
            return;
        }

        // Small semi-implicit Euler steps keep orbit drift low at variable FPS.
        int steps = Mathf.CeilToInt(Time.deltaTime * 120f);
        float step = Time.deltaTime / steps;
        Vector3 position = transform.position;
        for (int i = 0; i < steps; i++)
        {
            Vector3 offset = position - attractor.position;
            float distance = Mathf.Max(offset.magnitude, 0.5f);
            Vector3 acceleration = -gravity * offset / (distance * distance * distance);
            velocity += acceleration * step;
            position += velocity * step;
        }
        transform.position = position;
    }
}
