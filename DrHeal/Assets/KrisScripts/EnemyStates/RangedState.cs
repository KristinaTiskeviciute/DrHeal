using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState {
    private Enemy enemy;
    private float shootTimer;
    private float shootCooldown = 3;
    private bool canShoot=true;
    

    public void Execute()
    {
        
        Shoot();
        if (enemy.Target != null)
        {
            enemy.Move(2);
        }
        else
        {
            enemy.ChangeState(new IdleState());
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

    }
    private void Shoot()
    {
        
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            canShoot = true;
            shootTimer = 0;
        }
        if (canShoot)
        {
            
            canShoot = false;  
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
