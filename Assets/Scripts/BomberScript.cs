using UnityEngine;
using System.Collections;

public class BomberScript : MonoBehaviour {
    float Speed;
	// Use this for initialization
	void Start () {
        Speed = 1f;
        Vector2 Direction = PlayerScript.Instance.transform.position - transform.position;
        rigidbody2D.velocity = Direction.normalized * Speed;
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 Direction = PlayerScript.Instance.transform.position - transform.position;
        rigidbody2D.velocity = Direction.normalized * Speed;
        Speed += .001f;

        float XDiff = PlayerScript.Instance.transform.position.x- transform.position.x;
        float YDiff = PlayerScript.Instance.transform.position.y - transform.position.y;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI - 90);
        
	}
    
}
