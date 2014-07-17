using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {
    public GameObject MiddleObject;
    public GameObject Bullet;
    public int Ring;
    public float ShootCount;
    void Awake()
    {
        Ring = 1;
        ShootCount = 0;
    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Shoot();
        
	}
    
    void Shoot()
    {
        
        if (ShootCount == 0)
        {
           
            Collider2D[] Colds = Physics2D.OverlapCircleAll(transform.position, 1);
            int ENCount = 0;
            for (int i = 0; i < Colds.Length; i++)
            {
                if (Colds[i].tag == "Enemy")
                {
                    ENCount++;
                }
            }
                if (ENCount > 0)
                {

                    for (int i = 0; i < Colds.Length; i++)
                    {
                        if (Colds[i].gameObject.tag == "Enemy")
                        {
                            Vector3 Direction = Colds[i].transform.position - transform.position;
                            GameObject Bulet = Instantiate(Bullet) as GameObject;
                            Bulet.transform.position = transform.position;
                            Bulet.rigidbody2D.velocity = Direction * 2;
                            Bulet.transform.localScale = new Vector3(.2f, .2f, .2f);
                            float XDiff = Colds[i].transform.position.x - Bulet.transform.position.x;
                            float YDiff = Colds[i].transform.position.y - Bulet.transform.position.y;

                            Bulet.transform.eulerAngles = new Vector3(Bulet.transform.eulerAngles.x, Bulet.transform.eulerAngles.y, Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI - 90);

                        }
                    }
                    ShootCount = 5;
                }
        }

        if (ShootCount > 0)
        {
            ShootCount -= .5f * Time.deltaTime;
            if (ShootCount < 0)
            {
                ShootCount = 0;
            }
        }
    }

}
