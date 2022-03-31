using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circular_saw : MonoBehaviour
{
    // Start is called before the first frame update
    float rotateSpeed;
    void Start()
    {
        rotateSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -rotateSpeed), Space.World);
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            collision.GetComponent<Enemy>().GetHit(3);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Player" )
        {
            collision.GetComponent<PlayerController>().GetHit(1);
            Destroy(this.gameObject);
        }

    }
}
