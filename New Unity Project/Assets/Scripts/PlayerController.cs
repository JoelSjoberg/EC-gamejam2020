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

    float accelerationStartTime;

    public int fuelConsumptionAmount = 1;
    int fuelConsumptionAmountInBoost;
    int fuel; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxBoostSpeed = maxSpeed * 2;
        speedBeforeBoost = maxSpeed;
        drag = rb.drag;
        boostDrag = drag / 4.0f;
        fuelConsumptionAmountInBoost = fuelConsumptionAmount * 2;
        fuel = GameStats.fuel;
    }

    void ConsumeFuelIfNeeded()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            bool shouldConsumeFuel = Time.time - accelerationStartTime > 0.5;
            if (shouldConsumeFuel)
            {
                fuel -= Input.GetKey(KeyCode.LeftShift) ? fuelConsumptionAmountInBoost : fuelConsumptionAmount;
                accelerationStartTime = Time.time;
            }
        }
    }

    public int GetFuel()
    {
        return fuel;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            accelerationStartTime = Time.time;
            maxSpeed = maxBoostSpeed;
            rb.drag = boostDrag;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = speedBeforeBoost;
            rb.drag = drag;
        }
        
        // Rotate player in direction of movement, taken from https://answers.unity.com/questions/1409883/how-to-make-character-rotate-in-the-direction-of-m.html
        float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        ConsumeFuelIfNeeded();
        if (fuel <= 0) {
            GetComponent<PlayerHealth>().ResetToStart();
            fuel = GameStats.fuel;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * acceleration);
        
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        speed = rb.velocity.magnitude;
    }
}
