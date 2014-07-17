using UnityEngine;
using System.Collections;

public class SoundKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Kill", 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Kill()
    {
        Destroy(gameObject);
    }
}
