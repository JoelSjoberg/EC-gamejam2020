using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextLevel = "";

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") 
        {
            GameStats.hp += 1;
            SceneManager.LoadScene(nextLevel);
        }
    }
}
