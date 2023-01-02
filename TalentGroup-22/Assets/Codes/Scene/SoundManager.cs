using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager objInstance = null;
    [
        Header("BGM")
    ]
    public AudioSource Stage_1_Monster = null;
    public AudioSource Stage_1_No_Monster = null;
    [
        Header("SFX")
    ]
    public AudioSource Crystal_get = null;
    public AudioSource Game_over = null;
    public AudioSource walk = null;
    public AudioSource run = null;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start() 
    {
        #region Find BGM
        Stage_1_Monster = GameObject.Find("Stage_1_Monster").GetComponent<AudioSource>();
        Stage_1_No_Monster = GameObject.Find("Stage_1_No_Monster").GetComponent<AudioSource>();
        #endregion
        #region Find SFX
        Crystal_get = GameObject.Find("Crystal_get").GetComponent<AudioSource>();
        Game_over = GameObject.Find("Game_over").GetComponent<AudioSource>();
        walk = GameObject.Find("walk").GetComponent<AudioSource>();
        run = GameObject.Find("run").GetComponent<AudioSource>();
        walk.enabled = false;
        run.enabled = false;
        #endregion
    }
}