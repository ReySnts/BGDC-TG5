using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager objInstance = null;
    float pX = 0f;
    float pY = 0f;
    GameObject player = null;
    public void LoadData()
    {
        PlayerPrefs.SetInt
        (
            "TimeToLoad", 
            1
        );
        PlayerPrefs.Save();
    }
    void Awake()
    {
        objInstance ??= this;
        if (objInstance != this) Destroy(gameObject);
        else LoadData();
    }
    void Start()
    {
        player = Player.objInstance.gameObject;
        PlayerPrefs.SetInt
        (
            "SavedScene", 
            SceneManager.GetActiveScene().buildIndex
        );
        if 
        (
            PlayerPrefs.GetInt("Saved") == 1 
            && 
            PlayerPrefs.GetInt("TimeToLoad") == 1
        )
        {
            pX = player.transform.position.x;
            pY = player.transform.position.y;
            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            player.transform.position = new Vector2(pX, pY);
            PlayerPrefs.SetInt
            (
                "TimeToLoad", 
                0
            );
            PlayerPrefs.Save();
        }
    }
    public void SaveData()
    {
        pX = player.transform.position.x;
        pY = player.transform.position.y;
        PlayerPrefs.SetFloat
        (
            "p_x", 
            player.transform.position.x
        );
        PlayerPrefs.SetFloat
        (
            "p_y", 
            player.transform.position.y
        );
        PlayerPrefs.SetInt
        (
            "Saved", 
            1
        );
        PlayerPrefs.Save();
    }
    void Update()
    {
        if 
        (
            PlayerPrefs.GetInt("SavedScene") 
            == 
            SceneManager.GetActiveScene().buildIndex
        )
        SaveData();
    }
}