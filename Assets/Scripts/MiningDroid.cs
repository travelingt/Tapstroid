using UnityEngine;
using System.Collections;

public class MiningDroid : MonoBehaviour {
    public GameObject AsteroidIamEating;
    public MiningDroidState CurrentState;
	// Use this for initialization
    void Awake()
    {
    
    }
	void Start () {
      //  CurrentState = new MiningDroidState(this);
   
	}
	
	// Update is called once per frame
	void Update () {
        CurrentState.Update();
	}
    public void GatherFromAsteroid()
    {
      
        CurrentState = new TravelingToAsteroid(this);        
    }
    public class MiningDroidState
    {
        protected MiningDroid MD;
        public MiningDroidState(MiningDroid InMD)
        {
            MD = InMD;
        }
        public virtual void Update()
        {
            
        }
    }

    public class TravelingToAsteroid : MiningDroidState
    {
        public TravelingToAsteroid(MiningDroid InMd)
            : base(InMd)
        {

        }
        public override void Update()
        {
           
            if (MD.AsteroidIamEating != null)
            {
                float Dis = Vector3.Distance(MD.AsteroidIamEating.transform.position, MD.transform.position);
              //  if (Dis > 2)
                {
                    Vector3 Dir = (MD.AsteroidIamEating.transform.position - MD.transform.position).normalized;
                    MD.rigidbody2D.velocity = Dir * 2;
                }
            }
            else
            {
                BackgroundCreation.Instance.SendMiningDroid(MD.gameObject);
            }
            base.Update();
        }
        /*
        public override void Update()
        {
           
            
        }
         * */
    }

    public class Gathering : MiningDroidState
    {
        public Gathering(MiningDroid InMd)
            : base(InMd)
        {

        }
        public void Update()
        {

        }
    }
    public class ReturningToBase : MiningDroidState
    {
        public ReturningToBase(MiningDroid InMd)
            : base(InMd)
        {

        }
        public void Update()
        {

        }
    }
}
