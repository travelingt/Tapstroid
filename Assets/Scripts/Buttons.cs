using UnityEngine;
using System.Collections;
using LuaInterface;

public class Buttons : MonoBehaviour {
    
    //This two combined are what a button does
    
    //Action: Open, will open another Shop Context, defined by info, Buy will purchase the item that is defined by Info

    public string Action;
    
    public string Info;

    public ShopUI shopUI;

    public float cost;
    
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
    
    }
    void OnMouseUp()
    {
        
    }


}
