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

    // Start is called before the first frame update
    void Start()
    {
        mass = 1e10f; // With this you can still escape and "wrap around" the black hole
    }

    void OnTriggerStay2D(Collider2D collider2D) {
        Vector2 dist = transform.position - collider2D.transform.position;

        // object is "consumed" by the hole
        if (dist.magnitude < 0.01) { 
            Destroy(collider2D.gameObject);
            return;
        }

        // Calculate gravitional acceleration. The mass of the black hole should be massive so that 
        // it can affect any other object. 
        float r = dist.magnitude;
        float f = gconst * (mass * collider2D.attachedRigidbody.mass) / r;

        collider2D.attachedRigidbody.velocity += dist.normalized * f * Time.deltaTime;
    }
}
