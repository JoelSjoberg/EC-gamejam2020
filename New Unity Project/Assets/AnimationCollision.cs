using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCollision : MonoBehaviour
{
    Animator anim;
    void Start()
    {
    anim = GetComponent<Animator>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PLayerHitCheckpoint");
        if(other.tag == "Player") {
            anim.SetTrigger("Touched");

        }
    }
}
