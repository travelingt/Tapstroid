using UnityEngine;
using System.Collections;

public class RingIndicator : MonoBehaviour {
  
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        GameObject NewOrbitor = Instantiate(PlayerScript.Instance.OrbiterPrefab) as GameObject;
        NewOrbitor.transform.position = new Vector3();
        NewOrbitor.GetComponent<Orbiter>().Ring = transform.localScale.x;
        PlayerScript.Instance.AllOrbiters.Add(NewOrbitor.GetComponent<Orbiter>());
        PlayerScript.Instance.UpdatePositionOfObirters();
    }
}
