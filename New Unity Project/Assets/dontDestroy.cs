using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    private static bool exists = false;
    // Start is called before the first frame update
    void Start()
    {
        if (exists) 
        {
            Destroy(gameObject);
        }
        else 
        {
            exists = true;
            DontDestroyOnLoad(this.gameObject);
        } 
    }
}
