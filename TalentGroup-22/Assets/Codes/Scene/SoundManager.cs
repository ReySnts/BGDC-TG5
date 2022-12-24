using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager objInstance = null;
    public AudioSource Crystal_get = null;
    public AudioSource Game_over = null;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start() 
    {
        Crystal_get = GameObject.Find("Crystal_get").GetComponent<AudioSource>();
        Game_over = GameObject.Find("Game_over").GetComponent<AudioSource>();
    }
}