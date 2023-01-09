using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Shard : MonoBehaviour
{
    public static Shard instance;

    readonly string colliderName = "Player";
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;

    const string ShardData = "WasDestroyed";

    private void Start()
    {
        string WasDestroyed = PlayerPrefs.GetString(ShardData + transform.position.ToString());
        if (WasDestroyed == "true")
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
     //   if (PlayerPrefs.GetInt("CollectedShard") != 0)
       // {
         //   Destroy(gameObject);
        //}
        /**f (PlayerPrefs.GetInt("CollectedShard")== 1)
           {
               
           } else
           {
               DontDestroyOnLoad(gameObject);
           } */
    } 

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
                Collect();
                AudioManager.instance.ShardSFX();
            }


        }

    
        void Collect()
    {
        PlayerPrefs.SetString(ShardData + transform.position.ToString(), "true") ;
        gameObject.SetActive(false);
    }
        //PlayerPrefs.SetInt("CollectedShard", 1);
          //  Destroy(gameObject);

    public void ResetShard()
    {
        PlayerPrefs.DeleteKey(ShardData + transform.position.ToString());
    }
        }

        
    