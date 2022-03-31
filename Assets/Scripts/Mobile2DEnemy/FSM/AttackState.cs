using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 2;
       // enemy.TargetPoint = enemy.attackList[0];

        enemy.anim.Play("run");
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.hasBomb)
            return;
        if (enemy.attackList.Count == 0)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        if (enemy.attackList.Count > 1)
        {

            for (int i = 0; i < enemy.attackList.Count; i++)
            {

                if (Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) < Mathf.Abs(enemy.transform.position.x - enemy.TargetPoint.transform.position.x))
                {

                 
                    enemy.TargetPoint = enemy.attackList[i];


                }
            }
        }
        else if (enemy.attackList.Count == 1)
        {
            enemy.TargetPoint = enemy.attackList[0];
        }
        enemy.MoveToTarget();
        if (enemy.TargetPoint.CompareTag("Player"))
        {
            enemy.AttackAction();

        }

    }
}
