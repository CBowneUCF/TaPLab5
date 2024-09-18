using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public int health;
    //public float size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * 2f);

        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            whatIHit.GetComponent<Player>().DIE();
            Destroy(this.gameObject);
        } 
        else if (whatIHit.tag == "Laser")
        {
            Destroy(whatIHit.gameObject);
            health--;
            if(health <= 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().meteorCount++;
                
                Destroy(gameObject);
            }
        }
    }
}
