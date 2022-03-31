using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : laser
{
    public float attackRate;
    public float nextAttack;
    void Start()
    {
        player = GameObject.Find("Player");
        lineRederer = GetComponent<LineRenderer>();

        lineRederer.enabled = true;

        lineRederer.useWorldSpace = true;
        nextAttack = 0;
    } 
    public override void Update()
    {


        hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, layers);


        Debug.DrawLine(transform.position, hit.point);



        lineRederer.SetPosition(0, transform.position);

        lineRederer.SetPosition(1, hit.point);

        //激光触碰到玩家

        if (hit.collider.tag == "Player"&&Time.time>nextAttack+attackRate)

        {
            nextAttack = Time.time;
            player.GetComponent<PlayerController>().GetHit(1);

        }

        if (hit.collider.tag == "Enemy")
        {
            hit.collider.GetComponent<Enemy>().GetHit(3);
        }
        Invoke("destroy", 1);
        
    }
    void destroy()
    {
        Destroy(gameObject, 1);
    }
}
