using UnityEngine;
using System.Collections;

public class FailTextControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(.4f, .4f,1);
	}
	
	// Update is called once per frame
	void Update () {

        transform.localScale *= .9f;
        
        if(transform.localScale.x <=.1f)
        {
            Destroy(gameObject);
        }

        guiText.color = new Color(1, .6f, .6f);
	
         
    }
}
