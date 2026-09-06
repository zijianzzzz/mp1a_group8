using System;
using UnityEngine;

public class CometOrbit : MonoBehaviour
{
    public double gravity = 0.2;

    private static readonly Vector3 PlanetPosition = new Vector3(0f, 7.5f, 0f);
    private Vector3 velocity = new Vector3(0f, 0f, 0.2f);

    void Update()
    {
        Vector3 position = transform.position - PlanetPosition;

        double distance = Math.Sqrt(
            Math.Pow(position.x, 2) +
            Math.Pow(position.y, 2) +
            Math.Pow(position.z, 2)
        );
        distance = Math.Max(distance, 0.5);

        float ax = (float)(-gravity * position.x / Math.Pow(distance, 3));
        float ay = (float)(-gravity * position.y / Math.Pow(distance, 3));
        float az = (float)(-gravity * position.z / Math.Pow(distance, 3));

        velocity.x = velocity.x + ax * Time.deltaTime;
        velocity.y = velocity.y + ay * Time.deltaTime;
        velocity.z = velocity.z + az * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }
}
