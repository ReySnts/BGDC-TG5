using UnityEngine;
public class EnemyAIPatrol : EnemyAI
{
    public Vector2[] patrolPositions = new Vector2[4];
    int index = 0;
    protected override void Patrol()
    {
        if
        (
            Vector2.Distance
            (
                enemyBody.position,
                basePosition
            )
            <= wayPointDistance
        )
        {
            if (!hasReachedPatrolPosition)
            {
                hasReachedPatrolPosition = true;
                index++;
                if (index == patrolPositions.Length) index = 0;
            }
        }
        else hasReachedPatrolPosition = false;
        basePosition = patrolPositions[index];
    }
}