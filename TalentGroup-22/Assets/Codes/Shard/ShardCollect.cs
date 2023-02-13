using UnityEngine;
public class ShardCollect : MonoBehaviour
{
    public static ShardCollect instance = null;
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;
    const string ShardData = "WasDestroyed";
    public void ResetShard()
    {
        PlayerPrefs.DeleteKey
        (
            ShardData + transform.position.ToString()
        );
    }
    void Start()
    {
        string WasDestroyed = PlayerPrefs.GetString
        (
            ShardData + transform.position.ToString()
        );
        if 
        (
            WasDestroyed == "true"
        ) 
        gameObject.SetActive(false);
        else gameObject.SetActive(true);
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
            PlayerPrefs.SetString
            (
                ShardData + transform.position.ToString(), 
                "true"
            );
            Score.objInstance.addScore?.Invoke();
            AudioManager.instance.ShardCollectSFX();
            Destroy(gameObject);
        }
    }
}