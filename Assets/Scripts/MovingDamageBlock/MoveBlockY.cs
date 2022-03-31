using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockY : MoveBlockX
{

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }
    public override void DistanceToTarget()
    {
        if (Mathf.Abs(transform.position.y - currentTarget.position.y) < 0.01f)
        {
            TransitionToTarget();
        }
    }
    public override void TransitionToTarget()
    {
        if (Mathf.Abs(transform.position.y - TargetA.position.y) >
            Mathf.Abs(transform.position.y - TargetB.position.y))
        {
            currentTarget = TargetA;
        }
        else currentTarget = TargetB;
    }
}
