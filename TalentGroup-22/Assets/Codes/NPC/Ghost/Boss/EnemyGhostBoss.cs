using UnityEngine;
public class EnemyGhostBoss : EnemyGhost
{
    public GameObject splashPrefab = null;
    GameObject splashClone = null;
    float attackTime = 0f;
    float cooldown = 4f;
    float tan = 0f;
    float angleBetweenPlayer = 0f;
    protected override void Splash() 
    {
        if (Time.time > attackTime) 
        {
            attackTime = Time.time + cooldown;
            try
            {
                tan = Mathf.Atan2
                (
                    Player.objInstance.gameObject.transform.position.y,
                    Player.objInstance.gameObject.transform.position.x
                );
                angleBetweenPlayer = tan * Mathf.Rad2Deg + 90f;
                splashClone = Instantiate
                (
                    splashPrefab, 
                    transform.position, 
                    Quaternion.identity
                );
                splashClone.transform.Rotate
                (
                    0f, 
                    0f, 
                    angleBetweenPlayer
                );
            }
            catch{}
        }
    }
}