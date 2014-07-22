using UnityEngine;
using System.Collections;

public class RingIndicator : MonoBehaviour {
  
    
	// Use this for initialization
	void Start () {
        transform.parent = RingHolderScript.Instance.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        float Cost = RingHolderScript.Instance.LoadPrefabForOrbitor(RingHolderScript.Instance.CurrentBuyItem).GetComponent<Orbiter>().cost;
        
        
        if ((PlayerScript.Instance.Money - Cost) >= 0)
        {
            PlayerScript.Instance.Money -= Cost;
            GameObject NewOrbitor = Instantiate(RingHolderScript.Instance.LoadPrefabForOrbitor(RingHolderScript.Instance.CurrentBuyItem)) as GameObject;
            NewOrbitor.transform.position = new Vector3();
            NewOrbitor.GetComponent<Orbiter>().Ring = transform.localScale.x;
            PlayerScript.Instance.AllOrbiters.Add(NewOrbitor.GetComponent<Orbiter>());
            PlayerScript.Instance.UpdatePositionOfObirters(0);
        }
        else
        {
            GameObject NewText = Instantiate(PlayerScript.Instance.FailText) as GameObject;
            NewText.transform.position = PlayerScript.Instance.transform.position;
           
        }
    }
    
}
