using UnityEngine;

public class Shard : MonoBehaviour
{  
    readonly string colliderName = "Player";
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == colliderName) 
        {
            if (Time.time - lastCollectedTime < pauseDuration) return;
            else 
            {
                lastCollectedTime = Time.time;
                isTriggered = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == colliderName) isTriggered = false;
    }
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            Score.objInstance.addScore?.Invoke();
            Destroy(gameObject);
           
        }
        

    }
}