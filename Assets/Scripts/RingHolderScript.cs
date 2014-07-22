using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingHolderScript : MonoBehaviour {
    
    public string CurrentBuyItem;
    public Dictionary<string, GameObject> LoadedOrbitorsPrefabs;    
    public static RingHolderScript Instance;
   
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        LoadedOrbitorsPrefabs = new Dictionary<string, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public GameObject LoadPrefabForOrbitor(string OrbitorPRefabtoLoad)
    {
        if (!LoadedOrbitorsPrefabs.ContainsKey(OrbitorPRefabtoLoad))
        {
            GameObject LoadResource = (GameObject)Resources.Load(OrbitorPRefabtoLoad, typeof(GameObject));
            if (LoadResource == null)
            {
                return null;
            }
            LoadedOrbitorsPrefabs.Add(OrbitorPRefabtoLoad, LoadResource);
            
            return LoadResource;

        }
        else
        {
            return LoadedOrbitorsPrefabs[OrbitorPRefabtoLoad];
        }
    }

}
