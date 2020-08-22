using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeScritp : MonoBehaviour
{
    public Vector2 end;
    GameObject wormholeEnd; 

    // Start is called before the first frame update
    void Start()
    {
        wormholeEnd = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            collider.transform.position = wormholeEnd.transform.position;
        }
    }
}
