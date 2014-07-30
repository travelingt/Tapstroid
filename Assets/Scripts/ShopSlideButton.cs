using UnityEngine;
using System.Collections;

public class ShopSlideButton : MonoBehaviour {

    int TouchId;
    float MouseX;
    public float Slide;
    public float StartX;
    public bool DisplayIOn;
	// Use this for initialization
	void Start () {
        DisplayIOn = false;

        print(transform.position.x);
        StartX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        MouseX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).x;
	}
    void OnMouseDown()
    {
        
        for(int i = 0;i<Input.touchCount;i++)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                TouchId = Input.GetTouch(i).fingerId;
            }
        }
    }
    void OnMouseDrag()
    {
       
        float CurX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)).x;
        transform.position += new Vector3(CurX - MouseX, 0);
        if (transform.position.x < StartX)
        {
            transform.position = new Vector3(StartX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > (StartX + Slide))
        {
            transform.position = new Vector3(StartX + Slide, transform.position.y, transform.position.z);
            if (!DisplayIOn)
            {
                ShopUI.Instance.DisplayMenu("Main");
                DisplayIOn = true;
            }
        }
      
    }
}
