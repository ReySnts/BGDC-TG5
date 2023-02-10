using System.Collections;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    protected Transform target = null;
    protected Animator animator = null;
    protected float minRange = 1.125f;
    protected float maxRange = 5f;
    protected float speed = 0f;
    float aISpeed = 300f;
    float ghostSpeed = 1.5f;
    float bossSpeed = 0.3f;
    float minClamp = -1f;
    float maxClamp = 1f;
    float damageAmount = 20f;
    public static bool isWaitingToHit = false;
    protected bool isChasingPlayer = false;
    bool hasHitPlayer = false;
    protected Vector2 basePosition = Vector2.zero;
    Vector2 movement = Vector2.zero;
    void ResetSpeed(bool onEnable) 
    {
        switch(this)
        {
            case EnemyAI:
                speed = aISpeed;
                break;
            case EnemyGhostBoss:
                speed = bossSpeed;
                if (onEnable) 
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
    protected void SuccessFindPlayer()
    {
        isChasingPlayer = true;
        target = GameObject.Find("Player").transform;
    }
    void OnEnable() 
    {
        animator = GetComponent<Animator>();
        basePosition = transform.position;
        isWaitingToHit = false;
        ResetSpeed(true);
        SuccessFindPlayer();
    }
    IEnumerator HoldHit()
    {
        yield return new WaitForSeconds(2f);
        hasHitPlayer = false;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            if
            (
                Vector2.Distance
                (
                    transform.position,
                    target.position
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
                    isChasingPlayer
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
                            SoundManager.objInstance.Damaged.Play();
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
    }
    protected abstract void FollowTarget();
    protected abstract void BackToBase();
    protected void FailFindPlayer()
    {
        isChasingPlayer = false;
    }
    protected void CheckRange()
    {
        if (target != null)
        {
            if
            (
                Vector2.Distance
                (
                    transform.position,
                    target.position
                ) 
                >= minRange 
                &&
                Vector2.Distance
                (
                    transform.position,
                    target.position
                ) 
                <= maxRange
            ) 
            {
                if (this is EnemyGhost) SuccessFindPlayer();
                FollowTarget();
            }
            else if
            (
                Vector2.Distance
                (
                    transform.position,
                    target.position
                ) 
                > maxRange
            ) 
            {
                FailFindPlayer();
                BackToBase();
            }
        }
    }
    protected void SetAnim
    (
        float horizontal, 
        float vertical
    )
    {
        if (animator != null) 
        {
            animator.SetFloat
            (
                "Horizontal",
                movement.x = Mathf.Clamp
                (
                    horizontal,
                    minClamp,
                    maxClamp
                )
            );
            animator.SetFloat
            (
                "Vertical",
                movement.y = Mathf.Clamp
                (
                    vertical,
                    minClamp,
                    maxClamp
                )
            );
            animator.SetFloat
            (
                "Speed",
                movement.sqrMagnitude
            );
        }
    }
}