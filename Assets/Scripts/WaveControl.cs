using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class WaveControl : MonoBehaviour {
    public GameObject Asteroid;
    List<int[]> WaveData;
    public Dictionary<int, GameObject> EnemyHolding;
    int WaveCount;
    int inWaveCount;
	// Use this for initialization
    void Start()
    {
        
        
        WaveCount = 0;
        inWaveCount = 0;
        EnemyHolding = new Dictionary<int, GameObject>();
        WaveData = new List<int[]>();
      //  InvokeRepeating("SpawnEnemy", 0.01f, 5);
        TextAsset TA = (TextAsset)Resources.Load("WaveData", typeof(TextAsset));
        Stream STr = new MemoryStream(TA.bytes);
        StreamReader STRead = new StreamReader(STr);
        string line;
        while ((line = STRead.ReadLine()) != null)
        {
            string[] FirstSplit = line.Split(':');
            if (FirstSplit.Length > 1)
            {
                List<int> SingleWaveData = new List<int>();
                string[] Enemies = FirstSplit[1].Split(',');
                for (int i = 0; i < Enemies.Length; i++)
                {
                    SingleWaveData.Add(int.Parse(Enemies[i]));
                }
                WaveData.Add(SingleWaveData.ToArray());
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //Using the number from the text file load the enemy into the dictionary for quick access later, and so
    //we are not loading the same enemy every time
    public GameObject GetEnemy(int EnemyIdNum)
    {
        //The enemy has been loaded return it
        if (EnemyHolding.ContainsKey(EnemyIdNum))
        {
            return EnemyHolding[EnemyIdNum];
        }
        else
        {

            //If not try and load it
            GameObject LoadEnemy = (GameObject)Resources.Load("Enemy" + EnemyIdNum.ToString(), typeof(GameObject));
            if (LoadEnemy != null)
            {

                //If has been loaded it, place it in the dictionary and then return it
                EnemyHolding.Add(EnemyIdNum, LoadEnemy);
                return LoadEnemy;
            }
            else
            {
                //If it could not been loaded return nothing, need to come up with a beter idea for this
                return null;
            }
        }
    }

    //The function that spawns all enemies in the game least for the moment, might return to this later
    //TODO think about this again , might be a better way since it is not longer a tower defenese game
    public void SpawnEnemy()
    {

       //Getting what edge of the szcreen it should start on
        Vector3 WorldPos = BackgroundCreation.ScreenPosition();
        WorldPos.z = -Camera.main.transform.position.z;
        WorldPos = Camera.main.ScreenToWorldPoint(new Vector3(WorldPos.x, WorldPos.y, WorldPos.z));

        //Creating the corrent Enemy Based on the Data File, for the correct wave, and position on wave
        GameObject NewAsteroid = Instantiate(GetEnemy(WaveData[WaveCount][inWaveCount])) as GameObject;
        NewAsteroid.transform.position = WorldPos;

       // NewAsteroid.rigidbody2D.velocity = Velocity;
        //Going to the next enemy in the wave
        inWaveCount++;
        //If you have created all the enemies in a single wave go to the next one
        if (inWaveCount == WaveData[WaveCount].Length)
        {
            WaveCount++;
            inWaveCount = 0;

            //If you have spawned all waves restart //Debug mode, until acataul finish is in game
            if(WaveCount == WaveData.Count)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            
        }
    }
}
