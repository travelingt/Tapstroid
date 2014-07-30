using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    //Linked prefabs and childObjets/Scripts
    public GameObject BulletPrefab;
    public GameObject OrbiterPrefab;
    public GameObject ShootSoundPrefab;
    public ShopUI Shop;

    //List for the orbiters and the textfile that has the config of them
    public List<Orbiter> AllOrbiters;
    TextAsset OribterConfig;
    
    //Varibles for the basic money
    public GameObject MoneyText;
    public float Money;
    public GameObject FailText;

    //Singelton of the script
    public static PlayerScript Instance;

    //Varibles for the gun
    public float ShootCount;
    public float Clip;
    public int ClipSize;
    public float ShootCost;

    float OrbitorPosition;
    //Used Health Varible
    public float Health;

    public bool CanShoot;

    void Awake()
    {
        OrbitorPosition = 0;
        ClipSize = 10;
        Clip = ClipSize;
        ShootCost = 1;
        Instance = this;
        Money = 0;
        AllOrbiters = new List<Orbiter>();
        OribterConfig = (TextAsset)Resources.Load("OribterConfig", typeof(TextAsset));
        CanShoot = true;

        #region OLD
        string[] Lines = OribterConfig.text.Split('\n');
        for (int i = 0; i < Lines.Length; i++)
        {
            string[] SingleLine = Lines[i].Split(':');
           
            if (SingleLine.Length > 2)
            {
                string[] AllOText = SingleLine[1].Split(',');
                for (int u = 0; u < AllOText.Length; u++)
                {
                    string Tower = AllOText[u];
                    
                    switch (Tower[0])
                    {
                        case 'A':
                            GameObject Orb = Instantiate(OrbiterPrefab) as GameObject;
                            Orb.GetComponent<Orbiter>().MiddleObject = gameObject;
                            AllOrbiters.Add(Orb.GetComponent<Orbiter>());
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        #endregion

    }
    // Use this for initialization
    void Start()
    {
        Health = 100;
        ShootCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {

        //Called to move the little towers 
        UpdatePositionOfObirters(.001f);


            Shoot();//Try to shoot
        
        ///Update the amount of money the UI says the character has
        MoneyText.GetComponent<GUIText>().text = "Money: " + Money.ToString();

    }
    public void UpdatePositionOfObirters(float Increment)
    {
        for (int i = 0; i < AllOrbiters.Count; i++)
        {

            AllOrbiters[i].transform.position =
                new Vector3(Mathf.Cos(OrbitorPosition) * AllOrbiters[i].Ring,
                    Mathf.Sin(OrbitorPosition) * AllOrbiters[i].Ring,
                    transform.position.z) + transform.position;
        }
        OrbitorPosition += Increment;
    }
    void Shoot()
    {
        //If the clip has less energy that it can have in total add to it
        if (Clip < ClipSize)
        {
            Clip += 1 * Time.deltaTime;
            if (Clip > ClipSize)
            {
                //Make sure that it does not go above it
                Clip = ClipSize;
            }
        }

        //Does the player Click/Touch the screen
        if (Input.GetMouseButtonDown(0))
        {
            
            //Has enough energy in the clip to shoot
                if ((Clip - ShootCost) > 0)
                {
                    //Grapping the position of tap/click to a world point, getting the direction of it in referance to the player
                    //Then normalizing it
                    Vector3 TapPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    Vector3 Direction = TapPoint - transform.position;
                    Vector3 NormDirection = Direction.normalized;
                    
                    //Creating the bullet and firing it towards the tap posisition
                    GameObject Bullet = Instantiate(BulletPrefab) as GameObject;
                    Bullet.transform.position = gameObject.transform.position;
                    Bullet.rigidbody2D.velocity = NormDirection * 5;

                    //Rotating the buttet towards where it is going to move towards
                    float XDiff = TapPoint.x - Bullet.transform.position.x;
                    float YDiff = TapPoint.y - Bullet.transform.position.y;
                    Bullet.transform.eulerAngles = new Vector3(Bullet.transform.eulerAngles.x, Bullet.transform.eulerAngles.y, Mathf.Atan2(YDiff, XDiff) * 180 / Mathf.PI - 90);
                    
                    //Creating a shooting sound effect object takes care of itself after this point
                    GameObject NewSound = Instantiate(ShootSoundPrefab) as GameObject;
                    NewSound.transform.position = transform.position;
                    
                    //Set Slight delay in shooting and removing energy from clip
                    ShootCount = .5f;
                    Clip -= ShootCost;
                }
            
        }
        //if it cannot shoot get it to 
        if (ShootCount > 0)
        {
            ShootCount -= 1f * Time.deltaTime;
            if (ShootCount < 0)
            {
                ShootCount = 0;
            }
        }
    }


    public void DoDamage(float AttemptedDamage)
    {
        Health -= AttemptedDamage;
    }

    
}
