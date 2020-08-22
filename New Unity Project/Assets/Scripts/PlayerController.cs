using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velocity;
    public float maxSpeed;
    float speedBeforeBoost;
    float maxBoostSpeed;
    public float acceleration;
    public float speed;

    float drag, boostDrag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxBoostSpeed = maxSpeed * 2;
        speedBeforeBoost = maxSpeed;
        drag = rb.drag;
        boostDrag = drag / 4.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed = maxBoostSpeed;
            rb.drag = boostDrag;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = speedBeforeBoost;
            rb.drag = drag;
        }   
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * acceleration);
        
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        speed = rb.velocity.magnitude;
    }
}
