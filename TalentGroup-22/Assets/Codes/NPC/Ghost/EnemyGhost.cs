using UnityEngine;
public class EnemyGhost : Enemy
{
    protected override void FollowTarget()
    {
        MoveTo(target.position);
    }
    protected override void BackToBase()
    {
        MoveTo(basePosition);
    }
    void MoveTo(Vector3 target)
    {
        transform.position = Vector2.MoveTowards
        (
            transform.position, 
            target, 
            speed * Time.deltaTime
        );
        SetAnim
        (
            target.x - transform.position.x,
            target.y - transform.position.y
        );
    }
    protected virtual void Splash(){}
    void Update()
    {
        CheckRange();
        Splash();
    }
}