using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerData : MonoBehaviour
{
    public static PlayerData objInstance = null;
    GameObject player = null;
    Vector2 playerPos = Vector2.zero;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        #region Load
        PlayerPrefs.SetInt
        (
            "TimeToLoad", 
            1
        );
        PlayerPrefs.Save();
        #endregion
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
            playerPos.x = PlayerPrefs.GetFloat("p_x");
            playerPos.y = PlayerPrefs.GetFloat("p_y");
            player.transform.position = new Vector2
            (
                playerPos.x, 
                playerPos.y
            );
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
        if 
        (
            PlayerPrefs.GetInt("SavedScene") 
            == 
            SceneManager.GetActiveScene().buildIndex
        )
        {
            playerPos.x = player.transform.position.x;
            playerPos.y = player.transform.position.y;
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
    }
}