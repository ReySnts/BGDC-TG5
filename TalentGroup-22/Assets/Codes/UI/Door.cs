using UnityEngine;
public class Door : MonoBehaviour
{
    bool isOpen = false;
    bool notification = false;
    void Update()
    {
        AudioManager.instance.DoorSFX();
        if 
        (
            PlayerPrefs.GetInt("SavedScore") == 7 
            && 
            Input.GetKeyDown(KeyCode.F)
        )
        Destroy
        (
            gameObject, 
            5.0f
        );
        else DontDestroyOnLoad(gameObject);
    }
}
