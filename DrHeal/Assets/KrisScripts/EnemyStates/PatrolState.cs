using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 10;

    public void Execute()
    {  
        Patrol();   
        if (enemy.Target != null){
            enemy.ChangeState(new RangedState());
        }
    }
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Exit()
    {
    }
    public void OnTriggerEnter(Collider2D other)
    { 
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }
    private void Patrol()
    {
        enemy.Move(1);
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
