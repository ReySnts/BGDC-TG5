using UnityEngine;
public class ShardCollect : MonoBehaviour
{
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if 
            (
                other.name 
                == 
                Player.objInstance.gameObject.name
            ) 
            {
                if (Time.time - lastCollectedTime < pauseDuration) return;
                else 
                {
                    lastCollectedTime = Time.time;
                    isTriggered = true;
                }
            }
        }
        catch{}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            if 
            (
                other.name 
                == 
                Player.objInstance.gameObject.name
            ) 
            isTriggered = false;
        }
        catch{}
    }
    void Update()
    {
        if 
        (
            isTriggered 
            && 
            Input.GetKeyDown(KeyCode.F)
        )
        {
            Score.objInstance.addScore?.Invoke();
            SoundManager.objInstance.Crystal_get.Play();
            Destroy(gameObject);
        }
    }
}