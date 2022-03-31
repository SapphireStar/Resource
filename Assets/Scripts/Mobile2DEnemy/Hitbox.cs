using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public bool BombAvailable;
    int dir;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x)
            dir = -1;
        else dir = 1;
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamageable>().GetHit(1);

        }

    }
}
