using UnityEngine;
using System.Collections;

public class ShootUIMeterScript : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float Cross;
        Cross = PlayerScript.Instance.Clip/PlayerScript.Instance.ClipSize;
        transform.localScale = new Vector3(transform.localScale.x, Cross, 1);
	}
}
