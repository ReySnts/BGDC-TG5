using UnityEngine;
using Pathfinding;
public class EnemyAI : Enemy
{
    protected Seeker pathMaker = null;
    Path pathToTarget = null;
    protected Rigidbody2D enemyBody = null;
    protected float wayPointDistance = 1.5f;
    float followSpeed = 300f;
    float currentPointDistance = 0f;
    int currentTotalPathLine = 0;
    protected bool hasReachedPatrolPosition = false;
    protected bool hasLostTarget = false;
    protected bool hasStartedToPatrol = false;
    Vector2 currentPathPointPosition = Vector2.zero;
    Vector2 followDirection = Vector2.zero;
    Vector2 followForce = Vector2.zero;
    protected void Start()
    {
        pathMaker = GetComponent<Seeker>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyBody.gravityScale = 0f;
        minRange = wayPointDistance;
        InvokeRepeating
        (
            "UpdatePathOverTime",
            0f,
            0.5f
        );
    }
    void UpdatePathOverTime()
    {
        CheckRange();
    }
    protected override void FollowPlayer()
    {
        if (pathMaker.IsDone()) pathMaker.StartPath
        (
            enemyBody.position, 
            enemyTarget.position,
            CallAfterCalculating
        );
        hasLostTarget = true;
    }
    protected virtual void Patrol(){}
    protected override void BackToBase()
    {
        Patrol();
        if (pathMaker.IsDone()) pathMaker.StartPath
        (
            enemyBody.position, 
            basePosition,
            CallAfterCalculating
        );
    }
    protected void CallAfterCalculating(Path calculatedPath)
    {
        if (!calculatedPath.error)
        {
            pathToTarget = calculatedPath;
            currentTotalPathLine = 0;
        }
    }
    protected void FixedUpdate()
    {
        if 
        (
            pathToTarget != null 
            && 
            currentTotalPathLine < pathToTarget.vectorPath.Count
        ) 
        {
            currentPathPointPosition = (Vector2) pathToTarget.vectorPath[currentTotalPathLine];
            followDirection = (currentPathPointPosition - enemyBody.position).normalized;
            followForce = followDirection * followSpeed * Time.fixedDeltaTime;
            enemyBody.AddForce(followForce);
            currentPointDistance = Vector2.Distance
            (
                currentPathPointPosition, 
                enemyBody.position
            ); 
            if (currentPointDistance < wayPointDistance) currentTotalPathLine++;
        }
        SetAnim
        (
            followDirection.x,
            followDirection.y
        );
    }
}