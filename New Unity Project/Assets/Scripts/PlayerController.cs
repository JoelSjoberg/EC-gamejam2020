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
    public int startingFuel;
    
    [SerializeField]
    ParticleSystem particles;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxBoostSpeed = maxSpeed * 2;
        speedBeforeBoost = maxSpeed;
        drag = rb.drag;
        boostDrag = drag / 4.0f;
        fuelConsumptionAmountInBoost = fuelConsumptionAmount * 2;
        particles.gameObject.SetActive(false);
        GameStats.fuel = startingFuel;
    }

    void ConsumeFuelIfNeeded()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            bool shouldConsumeFuel = Time.time - accelerationStartTime > 0.5;
            if (shouldConsumeFuel)
            {
                GameStats.fuel -= Input.GetKey(KeyCode.LeftShift) ? fuelConsumptionAmountInBoost : fuelConsumptionAmount;
                accelerationStartTime = Time.time;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            particles.gameObject.SetActive(true);
            accelerationStartTime = Time.time;
            maxSpeed = maxBoostSpeed;
            rb.drag = boostDrag;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            particles.gameObject.SetActive(false);
            maxSpeed = speedBeforeBoost;
            rb.drag = drag;
        }
        
        // Rotate player in direction of movement, taken from https://answers.unity.com/questions/1409883/how-to-make-character-rotate-in-the-direction-of-m.html
        if(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude > 0f)
        {
            float angle = Mathf.Atan2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        ConsumeFuelIfNeeded();
        if (GameStats.fuel <= 0) {
            GetComponent<PlayerHealth>().ResetToStart();
            GameStats.fuel = startingFuel;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * acceleration);
        
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        speed = rb.velocity.magnitude;
    }
}
