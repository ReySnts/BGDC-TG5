using System.Collections;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    protected Transform enemyTarget = null;
    Animator enemyAnim = null;
    protected readonly string playerName = "Player";
    protected float minRange = 1.125f;
    protected float maxRange = 5f;
    protected float speed = 0f;
    float aISpeed = 300f;
    float ghostSpeed = 3f;
    float minClamp = -1f;
    float maxClamp = 1f;
    float damageAmount = 20f;
    public static bool isWaitingToHit = false;
    bool hasHitPlayer = false;
    protected Vector2 basePosition = Vector2.zero;
    Vector2 movement = Vector2.zero;
    void Awake()
    {
        enemyTarget = GameObject.Find(playerName).transform;
        enemyAnim = GetComponent<Animator>();
        if (this is EnemyGhost) speed = ghostSpeed;
        else speed = aISpeed;
        basePosition = transform.position;
        isWaitingToHit = false;
    }
    IEnumerator HoldHit()
    {
        yield return new WaitForSeconds(2f);
        hasHitPlayer = false;
    }
    void LateUpdate()
    {
        if
        (
            Vector2.Distance
            (
                transform.position,
                enemyTarget.position
            )
            <= minRange
        )
        {
            #region Stop Enemy
            speed = 0f;
            SetAnim
            (
                speed,
                speed
            );
            #endregion
            #region Attack Player
            if 
            (
                enemyTarget.gameObject.name == playerName
                &&
                !hasHitPlayer
                &&
                (
                    this is EnemyGhost
                    ||
                    (
                        this is EnemyAI
                        &&
                        !PlayerHide.objInstance.hasClicked
                    )
                )
            )
            {
                hasHitPlayer = true;
                isWaitingToHit = false;
                PlayerHealth.objInstance.currentHealth -= damageAmount;
                StartCoroutine
                (
                    HoldHit()
                );
            }
            else isWaitingToHit = true;
            #endregion
        }
        else
        {
            #region Move Enemy
            if (this is EnemyGhost) speed = ghostSpeed;
            else speed = aISpeed;
            #endregion
        }
    }
    protected abstract void FollowTarget();
    protected abstract void BackToBase();
    protected void CheckRange()
    {
        if
        (
            Vector2.Distance
            (
                transform.position,
                enemyTarget.position
            ) 
            >= minRange 
            &&
            Vector2.Distance
            (
                transform.position,
                enemyTarget.position
            ) 
            <= maxRange
        ) FollowTarget();
        else if
        (
            Vector2.Distance
            (
                transform.position,
                enemyTarget.position
            ) 
            > maxRange
        ) BackToBase();
    }
    protected void SetAnim
    (
        float horizontal, 
        float vertical
    )
    {
        enemyAnim.SetFloat
        (
            "Horizontal",
            movement.x = Mathf.Clamp
            (
                horizontal,
                minClamp,
                maxClamp
            )
        );
        enemyAnim.SetFloat
        (
            "Vertical",
            movement.y = Mathf.Clamp
            (
                vertical,
                minClamp,
                maxClamp
            )
        );
        enemyAnim.SetFloat
        (
            "Speed",
            movement.sqrMagnitude
        );
    }
}