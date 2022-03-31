using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamageable
{

    public SpriteRenderer sprite;
    public override void Init()
    {
        base.Init();
        sprite = GetComponent<SpriteRenderer>();

    }
    public override void Update()
    {
        base.Update();
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("Captain_skill"))
            sprite.flipX = false;
    }
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("Hit");
    }
    public override void SkillAction()
    {
        base.SkillAction();
        sprite.flipX = true;
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Captain_skill"))
        {
            if (TargetPoint.position.x < transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * speed, speed * 2 * Time.deltaTime);
            }
            else transform.position = Vector2.MoveTowards(transform.position, -transform.position + Vector3.right * speed, speed * 2 * Time.deltaTime);
        }
    }
}
