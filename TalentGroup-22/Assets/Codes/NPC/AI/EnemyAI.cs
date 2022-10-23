using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : Enemy
{
    Seeker pathMaker = null;
    Path pathToTarget = null;
    protected Rigidbody2D enemyBody = null;
    protected float wayPointDistance = 1.5f;
    float currentPointDistance = 0f;
    int currentTotalPathLine = 0;
    int index = 1;
    int lockerLen = 0;
    bool hasSetLockerDestination = false;
    bool isSearchingLocker = false;
    Vector2 currentPathPointPosition = Vector2.zero;
    Vector2 followDirection = Vector2.zero;
    Vector2 followForce = Vector2.zero;
    List<Transform> lockers = new List<Transform>();
    List<Transform> lockersInRange = new List<Transform>();
    string lockerName = null;
    string lockerNameBeingSearched = null;
    void Start()
    {
        #region Setup
        pathMaker = GetComponent<Seeker>();
        enemyBody = GetComponent<Rigidbody2D>();
        enemyBody.gravityScale = 0f;
        minRange = minRange * 4f / 3f;
        maxRange *= 2f;
        InvokeRepeating
        (
            "UpdatePathOverTime",
            0f,
            0.1875f
        );
        #endregion
        #region List All Lockers
        lockerName = Player.objInstance.lockerName;
        try
        {
            lockers.Add
            (
                GameObject.Find(lockerName).transform
            );
            lockerName += " (";
        }
        catch{}
        index = 1;
        while (index > 0)
        {
            try
            {
                lockers.Add
                (
                    GameObject.Find(lockerName + index + ")").transform
                );
                index++;
            }
            catch
            {
                break;
            }
        }
        lockerName = Player.objInstance.lockerName;
        lockerLen = lockerName.Length;
        #endregion
    }
    void OnCollisionStay2D(Collision2D other) 
    {
        lockerNameBeingSearched = 
        other.gameObject.name == target.gameObject.name ? 
        other.gameObject.name : null;
    }
    void OnCollisionExit2D(Collision2D other)
    {
        try
        {
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    lockerLen
                )
                == lockerName
            )
            lockerNameBeingSearched = null;
        }
        catch{}
    }
    IEnumerator SearchingLocker(string lockerNameBeingSearched)
    {
        yield return new WaitForSeconds(5f);
        isSearchingLocker = false;
        hasSetLockerDestination = false;
        if 
        (
            !PlayerHealth.objInstance.isDie
            &&
            lockerNameBeingSearched == Player.objInstance.lockerFullName
        )
        {
            PlayerHide.objInstance.fail?.Invoke();
            SuccessFindPlayer();
        }
    }
    void UpdatePathOverTime()
    {
        #region List All Lockers In Range
        lockersInRange.Clear();
        foreach (Transform locker in lockers) 
        {
            if 
            (
                Vector2.Distance
                (
                    transform.position,
                    locker.position
                )
                >= minRange
                &&
                Vector2.Distance
                (
                    transform.position,
                    locker.position
                )
                <= maxRange
            ) 
            lockersInRange.Add(locker);
        }
        #endregion
        #region Set Target
        if (PlayerHide.objInstance.hasClicked)
        {
            FailFindPlayer();
            if 
            (
                !hasSetLockerDestination
                &&
                lockersInRange.Count > 0
            )
            {
                hasSetLockerDestination = true;
                index = Random.Range
                (
                    0,
                    lockersInRange.Count
                );
                target = lockersInRange[index];
            }
        }
        else 
        {
            SuccessFindPlayer();
            if (hasSetLockerDestination) 
            {
                hasSetLockerDestination = false;
                StopCoroutine("SearchingLocker");
            }
        }
        #endregion
        #region Search Locker
        if 
        (
            hasSetLockerDestination 
            && 
            lockerNameBeingSearched != null
            && 
            !isSearchingLocker
        )
        {
            isSearchingLocker = true;
            StartCoroutine
            (
                SearchingLocker(lockerNameBeingSearched)
            );
        }
        #endregion
        CheckRange();
    }
    void CallAfterCalculating(Path calculatedPath)
    {
        if (!calculatedPath.error)
        {
            pathToTarget = calculatedPath;
            currentTotalPathLine = 0;
        }
    }
    protected override void FollowTarget()
    {
        if 
        (
            pathMaker.IsDone()
        ) 
        pathMaker.StartPath
        (
            enemyBody.position, 
            target.position,
            CallAfterCalculating
        );
    }
    protected virtual void Patrol(){}
    protected override void BackToBase()
    {
        Patrol();
        if 
        (
            pathMaker.IsDone()
        ) 
        pathMaker.StartPath
        (
            enemyBody.position, 
            basePosition, 
            CallAfterCalculating
        );
    }
    void FixedUpdate()
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
            followForce = followDirection * speed * Time.fixedDeltaTime;
            enemyBody.AddForce(followForce);
            currentPointDistance = Vector2.Distance
            (
                currentPathPointPosition, 
                enemyBody.position
            ); 
            if 
            (
                currentPointDistance 
                < 
                wayPointDistance
            ) 
            currentTotalPathLine++;
            SetAnim
            (
                followDirection.x,
                followDirection.y
            );
        }
    }
}