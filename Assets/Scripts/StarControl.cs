using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarControl : MonoBehaviour {
    public GameObject StarPrefab;
	
    // Use this for initialization
	void Start () {
        InvokeRepeating("CreateStar", .001f, .5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void CreateStar()
    {
        GameObject NewStar = Instantiate(StarPrefab) as GameObject;

        Vector3 NewPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height * Random.value, -Camera.main.transform.position.z));
        NewStar.transform.position = NewPos;
        NewStar.rigidbody2D.velocity = new Vector2(-1, 0);
        NewStar.transform.parent = transform;
    }
}
