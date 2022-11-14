using UnityEngine;
public class EnemyGhost : Enemy
{
    Transform player = null;
    string targetObj = "Player";
    float minRange = 1.125f;
    float maxRange = 5f;
    float speed = 3f;
    Vector3 basePosition = Vector3.zero;
    void Start()
    {
        player = GameObject.Find(targetObj).GetComponent<Transform>();
        enemyAnim = GetComponent<Animator>();
        basePosition = transform.position;
    }
    void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards
        (
            transform.position, 
            target, 
            speed * Time.deltaTime
        );
        SetAnim
        (
            target.x - transform.position.x,
            target.y - transform.position.y,
            movement.sqrMagnitude
        );
    }
    void Update()
    {
        if
        (
            Vector3.Distance
            (
                transform.position,
                player.position
            ) >= minRange 
            &&
            Vector3.Distance
            (
                transform.position,
                player.position
            ) <= maxRange
        ) MoveTo(player.position);
        else if 
        (
            Vector3.Distance
            (
                transform.position,
                basePosition
            ) == minRange
        ) enemyAnim.SetFloat
        (
            "Speed",
            minRange
        );
        else if
        (
            Vector3.Distance
            (
                transform.position,
                player.position
            ) > maxRange
        ) MoveTo(basePosition);
    }
}