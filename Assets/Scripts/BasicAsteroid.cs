using UnityEngine;
using System.Collections;

public class BasicAsteroid : MonoBehaviour {
    public GameObject DeathSound;
    public float ShiftWidth;
	// Use this for initialization
	void Start () {

        Vector3 Direction = PlayerScript.Instance.transform.position - transform.position;
        rigidbody2D.velocity = Direction.normalized;
        Vector2 Shift = new Vector2(Random.Range(-ShiftWidth,ShiftWidth), Random.Range(-ShiftWidth,ShiftWidth));
        rigidbody2D.velocity += Shift;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z +1f);
	}

    void OnDestory()
    {
        print("Im Dead");
    }
}
