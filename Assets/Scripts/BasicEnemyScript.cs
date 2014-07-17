using UnityEngine;
using System.Collections;

public class BasicEnemyScript : MonoBehaviour
{
    int TopHealth = 10;
    float Health;
    public string TypeName;
    public GameObject DeathGameObject;
    // Use this for initialization
    void Start()
    {
        
        Health = TopHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health <= 0)
        {
            PlayerScript.Instance.Money += 10;
            if (DeathGameObject)
            {
                GameObject DSG = Instantiate(DeathGameObject) as GameObject;
                DSG.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
    public void DoDamage(float StartDamage)
    {
        Health -= StartDamage;
    }
    public void OnCollisionEnter2D(Collision2D cold)
    {
        if ((cold.gameObject.tag == "Player"))
        {
            cold.gameObject.GetComponent<PlayerScript>().DoDamage(10);

        }
        if (TypeName == "Asteroid")
        {
            GameObject DSG = Instantiate(DeathGameObject) as GameObject;
            DSG.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
   

}