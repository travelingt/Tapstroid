using UnityEngine;
using System.Collections;

public class BrainOfGame : MonoBehaviour {
 
    public GameObject PlayerPrefab;
    public GameObject Moneytext;
	// Use this for initialization
	void Start () {
        GameObject Player = Instantiate(PlayerPrefab) as GameObject;
        Player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, -Camera.main.transform.position.z));
        
        Player.GetComponent<PlayerScript>().MoneyText = Moneytext;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

}
