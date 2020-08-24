using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleYParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.parent.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
