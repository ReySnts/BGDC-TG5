using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    /**
    readonly string colliderName = "Player";
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("SavedScore") == 7 && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
        }
    }

   /** void OnTriggerEnter2D(Collider2D other)
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
   */
    
}
