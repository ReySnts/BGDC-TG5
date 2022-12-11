using System.Collections;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    protected Transform enemyTarget = null;
    protected Animator enemyAnim = null;
    readonly string targetObj = "Player";
    protected float minRange = 1.125f;
    float maxRange = 5f;
    float minClamp = -1f;
    float maxClamp = 1f;
    float damageAmount = 20f;
    public static bool isHittingPlayer = false;
    bool hasHitPlayer = false;
    protected Vector2 movement = Vector2.zero;
    protected Vector2 basePosition = Vector2.zero;
    protected void Awake()
    {
        enemyTarget = GameObject.Find(targetObj).transform;
        enemyAnim = GetComponent<Animator>();
        basePosition = transform.position;
    }
    IEnumerator HoldHit()
    {
        yield return new WaitForSeconds(2f);
        hasHitPlayer = false;
    }
    protected void LateUpdate()
    {
        if
        (
            !hasHitPlayer
            &&
            Vector2.Distance
            (
                transform.position,
                enemyTarget.position
            )
            <= minRange
        )
        {
            hasHitPlayer = isHittingPlayer = true;
            PlayerHealth.objInstance.currentHealth -= damageAmount;
            StartCoroutine
            (
                HoldHit()
            );
        }
        else isHittingPlayer = false;
    }
    protected abstract void FollowPlayer();
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
        ) FollowPlayer();
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