using UnityEngine;
using System.Collections;

public class BackGroundShopUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Object[] Help = Resources.LoadAll("A");
        for (int i = 0; i < Help.Length;i++ )
        {
            print(Help[i].ToString());
        }
            transform.localScale = Vector3.one;
        float width = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float height = GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(worldScreenWidth/width, (worldScreenHeight/height) * .1f, 1);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
