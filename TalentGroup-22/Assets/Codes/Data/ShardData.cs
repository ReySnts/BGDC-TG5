using System;
using System.Collections.Generic;
using UnityEngine;
public class ShardData : MonoBehaviour
{
    public static ShardData objInstance = null;
    public const string key = "Shard: ";
    public Action<GameObject, int> registerShard = null;
    public Action<GameObject, int> disactivateShard = null;
    const int totalShards = 7;
    List<GameObject> list = new List<GameObject>();
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void ActivateShard
    (
        bool isTrue, 
        int index
    )
    {
        PlayerPrefs.SetString
        (
            key + index,
            isTrue ? "1" : "0"
        );
    }
    void ActivateShard(int index)
    {
        ActivateShard
        (
            true, 
            index
        ); 
    }
    void DisactivateShard
    (
        GameObject shard, 
        int index
    )
    {
        bool isFalse = false;
        ActivateShard
        (
            isFalse,
            index
        );
        shard.SetActive(isFalse);
    }
    void RegisterShard
    (
        GameObject shard, 
        int index
    )
    {
        ActivateShard(index);   
        list.Add(shard);
    }
    public void ResetAll()
    {
        #region Reset Keys
        for 
        (
            int index = 0;
            index < totalShards;
            index++
        )
        ActivateShard(index);
        #endregion
        // #region Reset Score
        // PlayerPrefs.SetInt
        // (
        //     "SavedScore", 
        //     0
        // );
        // #endregion
    }
    void OnEnable()
    {
        registerShard += RegisterShard;
        disactivateShard += DisactivateShard;
    }
    void OnDisable()
    {
        registerShard -= RegisterShard;
        disactivateShard -= DisactivateShard;
    }
}