using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockX : MovingDamageBlock
{
    public Transform TargetA;
    public Transform TargetB;
    public float speed;
    public Transform currentTarget;
    public Transform checkPoint;
    public float radius;
    public bool playerIsOn;
    public LayerMask PlayerLayer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isMovingDamageBlock(this);
        }
        TransitionToTarget();
        currentTarget = TargetA;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {

        Movement();
        DistanceToTarget();
    }
    public virtual void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }
    public virtual void DistanceToTarget()
    {
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < 0.01f)
        {
            TransitionToTarget();
        }
    }
    public virtual void TransitionToTarget()
    {
        if (Mathf.Abs(transform.position.x - TargetA.position.x) >
            Mathf.Abs(transform.position.x - TargetB.position.x))
        {
            currentTarget = TargetA;
        }
        else currentTarget = TargetB;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        checkPlayerIsOn();
       if (playerIsOn)
        {
            collision.gameObject.transform.SetParent(transform);

        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(transform.parent.parent);
    }


    void checkPlayerIsOn()
    {
       playerIsOn=Physics2D.OverlapCircle(checkPoint.position, radius,PlayerLayer);
    }

}
