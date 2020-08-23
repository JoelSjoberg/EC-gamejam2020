using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCenter : MonoBehaviour
{
    [SerializeField]
    public Transform center;
    public float degrees;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(center.position, Vector3.forward, degrees * Time.fixedDeltaTime);
    }
}
