using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Kill", 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<BasicEnemyScript>().DoDamage(10);
            
        }
        Destroy(gameObject);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
