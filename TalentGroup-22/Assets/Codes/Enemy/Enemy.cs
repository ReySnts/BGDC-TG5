using UnityEngine;
public class Enemy : MonoBehaviour
{
    protected Animator enemyAnim = null;
    float minClamp = -1f;
    float maxClamp = 1f;
    protected Vector3 movement = Vector3.zero;
    protected void SetAnim
    (
        float horizontal, 
        float vertical, 
        float speed
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
            speed
        );
    }
}