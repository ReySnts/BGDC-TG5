using UnityEngine;
using Pathfinding;
public class EnemyAI : Enemy
{
    AIPath aIPath = null;
    void Start()
    {
        aIPath = GetComponent<AIPath>();
        enemyAnim = GetComponent<Animator>();
    }
    void Update()
    {
        SetAnim
        (
            aIPath.desiredVelocity.x,
            aIPath.desiredVelocity.y,
            aIPath.maxSpeed
        );
    }
}