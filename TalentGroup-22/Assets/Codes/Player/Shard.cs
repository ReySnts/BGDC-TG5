using UnityEngine;
using UnityEngine.UI;

public class Shard : MonoBehaviour
{  
    readonly string colliderName = "Player";
    bool isTriggered = false;
    static float lastCollectedTime = 0f;
    readonly float pauseDuration = 1f;

    const string ShardSaveData = "WasDestroyed";

    private void Start()
    {
        if (PlayerPrefs.GetInt("CollectedSharp" + transform.position.ToString()) != 0)
        {
            Destroy(gameObject);
        }
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
            PlayerPrefs.SetInt("CollectedSharp" + transform.position.ToString(), 1);
            Destroy(gameObject);
        }
    }