using UnityEngine;
public class EnemyAIPatrol : EnemyAI
{
    public Vector2[] patrolPositions = new Vector2[]{};
    bool hasReachedPatrolPosition = false;
    int patrolIndex = 0;
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
                patrolIndex++;
                if (patrolIndex == patrolPositions.Length) patrolIndex = 0;
            }
        }
        else hasReachedPatrolPosition = false;
        basePosition = patrolPositions[patrolIndex];
    }
}