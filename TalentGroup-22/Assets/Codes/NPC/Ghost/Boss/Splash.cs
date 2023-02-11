using UnityEngine;
public class Splash : MonoBehaviour
{
    Vector2 playerPosition = Vector2.zero;
    public static bool hasHitPlayer = false;
    float speed = 3f;
    float damageAmount = 50f;
    void OnEnable()
    {
        try
        {
            playerPosition = Player.objInstance.gameObject.transform.position;
        }
        catch{}
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        try
        {
            if 
            (
                other.gameObject.name
                ==
                Player.objInstance.gameObject.name
            )
            {
                hasHitPlayer = true;
                PlayerHealth.objInstance.currentHealth -= damageAmount;
                // SoundManager.objInstance.Explode_sound.Play();
                Destroy(gameObject);
            }
        }
        catch{}
    }
    void Update()
    {
        if 
        (
            transform.position
            !=
            (Vector3) playerPosition
        )
        transform.position = Vector2.MoveTowards
        (
            transform.position,
            playerPosition,
            speed * Time.deltaTime
        );
        else Destroy(gameObject);
    }
}