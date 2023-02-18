using UnityEngine;
public class ShardCollect : MonoBehaviour
{
    public static ShardCollect instance = null;
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;
    const string ShardData = "WasDestroyed";
    void Start()
    {
        gameObject.SetActive
        (
            PlayerPrefs.GetString
            (
                ShardData + transform.position.ToString()
            ) 
            == "true" ? true : false
        );
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if 
        (
            Player.objInstance != null 
            && 
            other.name == Player.objInstance.name
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
    void OnTriggerExit2D(Collider2D other)
    {
        if 
        (
            Player.objInstance != null 
            &&
            other.name == Player.objInstance.name
        ) 
        isTriggered = false;
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
            AudioManager.instance.ShardCollectSFX();
            PlayerPrefs.SetString
            (
                ShardData + transform.position.ToString(), 
                "true"
            );
            Destroy(gameObject);
        }
    }
}