using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundCreation : MonoBehaviour {
    public GameObject Prefab_Asteroid;
    public List<GameObject> AsteroidOutInWorld;
    
	// Use this for initialization
	void Start () {
        AsteroidOutInWorld = new List<GameObject>();
        InvokeRepeating("AsteroidSpawn", .001f, 1);
    }
	
	// Update is called once per frame
	void Update () {
       
        for (int i = 0; i < AsteroidOutInWorld.Count; i++)
        {
            if (AsteroidOutInWorld[i])
            {
                float Dist = Vector3.Distance(AsteroidOutInWorld[i].transform.position, PlayerScript.Instance.transform.position);
                if (Dist > 10)
                {
                    AsteroidOutInWorld[i].GetComponent<BasicEnemyScript>().DeathGameObject = null;
                    Destroy(AsteroidOutInWorld[i]);
                }
            }
            else
            {
                AsteroidOutInWorld.RemoveAt(i);
            }
        }
	}
    public static Vector2 ScreenPosition()
    {
        Vector2 Start = new Vector2(Random.value * Screen.width, Random.value * Screen.height);
        
        int Rand = Random.Range(0, 2);
        int SecRand = Random.Range(0, 2);
        if (Rand == 1)
        {
            Start.x = SecRand * Screen.width;
        }
        else
        {
            Start.y = SecRand * Screen.height;
        }

        return Start;
    }
    public void AsteroidSpawn()
    {
        GameObject NewAsteroid = Instantiate(Prefab_Asteroid) as GameObject;
        NewAsteroid.GetComponent<BasicEnemyScript>().TypeName = "Asteroid";
        
        Vector2 ScrPos = ScreenPosition();
       
        Vector3 Pos = Camera.main.ScreenToWorldPoint(new Vector3(ScrPos.x, ScrPos.y, -Camera.main.transform.position.z));
        NewAsteroid.transform.position = Pos;
        AsteroidOutInWorld.Add(NewAsteroid);
    }
}
