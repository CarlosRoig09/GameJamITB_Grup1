using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class EnemyBehaviour
{
    public abstract void StateEnter(Enemy enemy);
    public abstract void StateUpdate(Enemy enemy);
    public abstract void StateExit(Enemy enemy);
}
public class EnemyFollowing : EnemyBehaviour
{
    public override void StateEnter(Enemy enemy)
    {
        if(!enemy.SearchNewPlant()) enemy.ChangeState(enemy.leaving);
    }
    public override void StateUpdate(Enemy enemy)
    {
        if (enemy.plantObjective != null)
        {
            enemy.rb.velocity = (enemy.plantObjective.transform.position - enemy.transform.position).normalized * enemy.speed;
            if (Vector2.Distance(enemy.plantObjective.transform.position, enemy.transform.position) < 1)
            {
                enemy.ChangeState(enemy.leaving);
                Object.Destroy(enemy.plantObjective);
            }
            return;
        }
        if(!enemy.SearchNewPlant()) enemy.ChangeState(enemy.leaving);

    }
    public override void StateExit(Enemy enemy)
    {
    }
}
public class EnemyLeaving : EnemyBehaviour
{
    public override void StateEnter(Enemy enemy)
    {
        Vector2 randomPos = enemy.transform.position.normalized * enemy.speed;
        enemy.rb.velocity = randomPos;
    }
    public override void StateUpdate(Enemy enemy)
    {
        
    }
    public override void StateExit(Enemy enemy)
    {
        Object.Destroy(enemy.gameObject);
    }
}

