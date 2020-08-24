using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    Text healthString;

    // Start is called before the first frame update
    void Start()
    {
        healthString = GetComponent<Text>();
        healthString.text = GameStats.hp.ToString();
    }

    // This is not nice. Now this is updated many times even though not changing. 
    public void Update()
    {
        healthString.text = GameStats.hp.ToString(); // ugly
    }
}