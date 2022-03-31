using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDamageBlock : MovingDamageBlock
{
    public Transform center;
    public float speed;
    public float radius;
    // Start is called before the first frame update
    public override void Start()
    {
        transform.localPosition = new Vector3(0, radius, 0);
        OriginalPosition =transform.position ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    public void Movement()
    {
        transform.RotateAround(center.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
    }
}
