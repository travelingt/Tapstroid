using UnityEngine;
using System.Collections;

public class EnBulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
        float XDiff = PlayerScript.Instance.transform.position.x - transform.position.x;
        float YDiff = PlayerScript.Instance.transform.position.y - transform.position.y;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI - 90);
        Vector2 Vel = (PlayerScript.Instance.transform.position - transform.position).normalized;
        rigidbody2D.velocity = Vel * 2;
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerScript>().DoDamage(10);
        }
        Destroy(gameObject);
    }
}
