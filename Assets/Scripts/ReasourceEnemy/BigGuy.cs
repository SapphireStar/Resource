using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : Enemy, IDamageable
{
    public Transform BombPosition;
    public float throwPower;


    public void pickUpBomb()
    {
        if (TargetPoint.tag == "Bomb")
        {
            TargetPoint.position = BombPosition.position;
            TargetPoint.SetParent(BombPosition);
            TargetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        hasBomb = true;
    }
    public void throwBomb()
    {
        if (hasBomb)
        {
            TargetPoint.SetParent(transform.parent.parent);
            TargetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            if (player.position.x < transform.position.x)
                TargetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * throwPower, ForceMode2D.Impulse);
            else TargetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * throwPower, ForceMode2D.Impulse);
        }
        hasBomb = false;
    }

}
