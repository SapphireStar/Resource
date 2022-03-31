using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, -transform.up,10f,1);

        Debug.DrawLine(transform.position, hit.point);
        if (hit.collider.tag == "floor")
        {
            Debug.Log("碰到floor");
        }
    }
}
