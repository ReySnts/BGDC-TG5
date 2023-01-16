using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen;
    bool notification;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.instance.DoorSFX();
        if (PlayerPrefs.GetInt("SavedScore") == 7 && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject, 5.0f);
        } else
        {                DontDestroyOnLoad(gameObject);
        }
        
    }

    

}
