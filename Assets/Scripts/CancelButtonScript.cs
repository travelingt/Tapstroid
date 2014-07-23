using UnityEngine;
using System.Collections;

public class CancelButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * .1f, Screen.height * .1f, -Camera.main.transform.position.z));
        transform.position = NewPos;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {

       
    }
}
