using System.Collections;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    protected Transform enemyTarget = null;
    protected Animator enemyAnim = null;
    protected float minRange = 1.125f;
    protected float maxRange = 5f;
    protected float speed = 0f;
    float aISpeed = 300f;
    float ghostSpeed = 3f;
    float bossSpeed = 0.3f;
    float minClamp = -1f;
    float maxClamp = 1f;
    float damageAmount = 20f;
    public static bool isWaitingToHit = false;
    bool hasHitPlayer = false;
    protected Vector2 basePosition = Vector2.zero;
    Vector2 movement = Vector2.zero;
    void ResetSpeed(bool isAwake) 
    {
        switch(this)
        {
            case EnemyAI:
                speed = aISpeed;
                break;
            case EnemyGhostBoss:
                speed = bossSpeed;
                if (isAwake) 
                {
                    minRange = 4f;
                    maxRange *= 10f;
                }
                break;
            case EnemyGhost:
                speed = ghostSpeed;
                break;
        }
    }
    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        basePosition = transform.position;
        isWaitingToHit = false;
        ResetSpeed(true);
    }
    void OnEnable() 
    {
        try
        {
            enemyTarget = Player.objInstance.gameObject.transform;
        }
        catch{}
    }
    IEnumerator HoldHit()
    {
        yield return new WaitForSeconds(2f);
        hasHitPlayer = false;
    }
    void LateUpdate()
    {
        try
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
                #region Melee Attack
                if 
                (
                    enemyTarget.gameObject.name == Player.objInstance.gameObject.name
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
                    #region Melee Damage
                    switch(this)
                    {
                        case EnemyGhostBoss:
                            PlayerHealth.objInstance.currentHealth = PlayerHealth.objInstance.minimumHealth;
                            break;
                        default:
                            PlayerHealth.objInstance.currentHealth -= damageAmount;
                            StartCoroutine
                            (
                                HoldHit()
                            );
                            break;
                    }
                    #endregion
                }
                else isWaitingToHit = true;
                #endregion
            }
            else ResetSpeed(false);
        }
        catch{}
    }
    protected abstract void FollowTarget();
    protected abstract void BackToBase();
    protected void CheckRange()
    {
        try
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
        catch{}
    }
    protected void SetAnim
    (
        float horizontal, 
        float vertical
    )
    {
        if (enemyAnim != null) 
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
}