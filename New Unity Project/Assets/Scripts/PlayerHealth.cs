using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    Vector2 startpos;

    private void Start() {
        startpos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
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
            transform.position = startpos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Dangerous") { // Some are only triggers, such as black holes
            ResetToStart();
        }

        if (other.transform.tag == "Checkpoint") 
        {
            Debug.Log("Checkpoint!");
            startpos = other.transform.position;
        }
    }
}
