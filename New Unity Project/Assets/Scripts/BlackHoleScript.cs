using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Black holes that currently affect anyhting inside its radius. 
/// </summary>
public class BlackHoleScript : MonoBehaviour
{
    float gconst = 6.674e-11f; // From wikipedia
    public float mass; // This mass is only used for calculation of gravitational acceleration and not actually in physics
    public float orbitingSpeed = 20; // Angles per second

    public List<GameObject> orbiters;

    void OnTriggerStay2D(Collider2D collider2D) {
        Vector2 dist = transform.position - collider2D.transform.position;

        // object is "consumed" by the hole
        if (dist.magnitude < 0.01 && collider2D.gameObject.tag != "Player") { 
            Destroy(collider2D.gameObject);
            return;
        }

        // Calculate gravitional acceleration. The mass of the black hole should be massive so that 
        // it can affect any other object. 
        float r = dist.magnitude;
        float f = gconst * (mass * collider2D.attachedRigidbody.mass) / r;

        collider2D.attachedRigidbody.velocity += dist.normalized * f * Time.deltaTime;
    }

    void Update()
    {
        foreach (var orb in orbiters) {
            orb.transform.RotateAround(transform.position, new Vector3(0, 0, 1), orbitingSpeed * Time.deltaTime);
        }
    }
}
