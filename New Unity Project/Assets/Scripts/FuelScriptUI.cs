using UnityEngine;
using UnityEngine.UI;

public class FuelScriptUI : MonoBehaviour
{
    Text fuelString;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        Text textMesh = GetComponent<Text>();
        fuelString = textMesh;
        fuelString.text = GameStats.fuel.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // This is not nice. Now this is updated many times even though not changing. 
    public void Update()
    {
        fuelString.text = player.GetComponent<PlayerController>().GetFuel().ToString(); // ugly
    }
}
