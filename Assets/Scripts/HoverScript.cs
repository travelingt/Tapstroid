using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour {
    public EnemyState ES;
    public GameObject EnBulletPrefab; 
    public float ShootCounter;
	// Use this for initialization
	void Start () {
        Vector2 NewVel = (PlayerScript.Instance.transform.position - transform.position).normalized;
        ES = new MoveTowardsPlayer(this);
        ShootCounter = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
        ES.Update();
	}
    public abstract class EnemyState
    {
        protected HoverScript HS;
        public EnemyState(HoverScript HS)
        {
            this.HS = HS;
        }
        public virtual void Update()
        {

        }

       
    }
    public class MoveTowardsPlayer : EnemyState
    {
        public MoveTowardsPlayer(HoverScript H_S)
            : base(H_S)
        {
            
        }
        public override void Update()
        {
            if (Vector3.Distance(HS.transform.position, PlayerScript.Instance.transform.position) > 3)
            {
                HS.rigidbody2D.velocity = (PlayerScript.Instance.transform.position - HS.transform.position).normalized * 2;
            }
            else
            {
                HS.ES = new StopAndFire(HS);   
            }
            base.Update();
        }
    }
    public class StopAndFire : EnemyState
    {
        float Angle;
        float Range;
        public StopAndFire(HoverScript H_S)
            : base(H_S)
        {
            H_S.rigidbody2D.velocity = Vector2.zero;

            float XDiff = PlayerScript.Instance.transform.position.x - HS.transform.position.x;
            float YDiff = PlayerScript.Instance.transform.position.y - HS.transform.position.y;

            HS.transform.eulerAngles = new Vector3(HS.transform.eulerAngles.x, HS.transform.eulerAngles.y, Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI);
            Angle = HS.transform.eulerAngles.z;
            Range = Vector3.Distance(HS.transform.position, PlayerScript.Instance.transform.position);
           //   Angle += 90;
        }

        public override void Update()
        {
            if (Vector3.Distance(HS.transform.position, PlayerScript.Instance.transform.position) > 3)
            {
                HS.ES = new MoveTowardsPlayer(HS);
            }
            else
            {
                HS.ShootCounter += .1f * Time.deltaTime;
                if (HS.ShootCounter >= 2.5f)
                {
                    print("G");
                    GameObject Bul = Instantiate(HS.EnBulletPrefab) as GameObject;
                    Bul.transform.position = HS.transform.position;
                    HS.ShootCounter = 0;
                }
                /*
                float XDiff = PlayerScript.Instance.transform.position.x - HS.transform.position.x;
                float YDiff = PlayerScript.Instance.transform.position.y - HS.transform.position.y;

                HS.transform.eulerAngles = new Vector3(HS.transform.eulerAngles.x, HS.transform.eulerAngles.y,
                    Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI - 90);

                HS.transform.position = new Vector3(Range * Mathf.Cos(Angle) + PlayerScript.Instance.transform.position.x,
                    Range * Mathf.Sin(Angle) + PlayerScript.Instance.transform.position.y, HS.transform.position.z);
              
                Angle += .1f * Time.deltaTime;
                 * */
            }
            base.Update();
        }
    }

    
}
