using System;
using UnityEngine;
public class ShardCollect : MonoBehaviour
{
    public static ShardCollect instance = null;
    bool isTriggered = false;
    bool isDone = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;
    string objectName = "Shard (";
    string getKey = null;
    int objectIndex = 0;
    void Start()
    {
        #region Get Object Index
        objectIndex = Int32.Parse
        (
            gameObject.name.Substring
            (
                objectName.Length,
                1
            )
        );
        #endregion
        #region Check Key
        try
        {
            getKey = PlayerPrefs.GetString
            (
                ShardData.key 
                + 
                objectIndex
            );
        }
        catch
        {
            getKey = null;
        }
        #endregion
        if (getKey != null)
        {
            #region Activate
            if (getKey == "1") gameObject.SetActive(true);
            else if (getKey == "0") gameObject.SetActive(false);
            #endregion
        }
        else 
        {
            #region Register Shard
            ShardData.objInstance.registerShard?.Invoke
            (
                gameObject, 
                objectIndex
            );
            #endregion
        }
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
            !isDone 
            && 
            isTriggered 
            && 
            Input.GetKeyDown(KeyCode.F)
        )
        {
            isDone = true;
            Score.objInstance.addScore?.Invoke();
            AudioManager.instance.ShardCollectSFX();
            ShardData.objInstance.disactivateShard?.Invoke
            (
                gameObject, 
                objectIndex
            );
        }
    }
}