using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    Vector2 startpos;


    // Things needed for the player to lerp back to starting position
    bool movingBack = false;
    float deathTime;
    Vector3 deadPosition;
    float journeyLength;
    public float lerpSpeed;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    [SerializeField]
    ParticleSystem smog;

    private void Start() {
        startpos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        smog.gameObject.SetActive(false);
    }

    private void Update()
    {
        // https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        if (movingBack) { 
            float distCovered = (Time.time - deathTime) * lerpSpeed;
            float fractionJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(deadPosition, startpos, fractionJourney);

            bool backAtStart = Mathf.Approximately(transform.position.x, startpos.x) && 
                               Mathf.Approximately(transform.position.y, startpos.y);
            if (backAtStart) {
                movingBack = false;
                rb.isKinematic = false;
                boxCollider.isTrigger = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        smog.gameObject.SetActive(true);
        smog.Play();
        if (other.gameObject.tag == "Dangerous")
        {
            ResetToStart();
        }
    }

    public void ResetToStart()
    {
        GameStats.hp -= 1;

        // Dead if hp <= 0
        if (GameStats.hp <= 0)
        {
            // redirect to game over
        }
        else
        {
            // Return player to start
            movingBack = true;
            deathTime = Time.time;
            deadPosition = transform.position;
            journeyLength = (transform.position - new Vector3(startpos.x, startpos.y, 0)).magnitude;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            boxCollider.isTrigger = true; // Just to avoid some collisions
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Dangerous") { // Some are only triggers, such as black holes
            smog.gameObject.SetActive(true);
            smog.Play();
            ResetToStart();
        }

        if (other.transform.tag == "Checkpoint") 
        {
            Debug.Log("Checkpoint!");
            startpos = other.transform.position;
        }
    }
}
