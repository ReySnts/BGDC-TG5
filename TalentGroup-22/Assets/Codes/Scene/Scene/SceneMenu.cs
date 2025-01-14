using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMenu : Scene
{
    public static SceneMenu objInstance = null;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        UnfreezeTime();
    }
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    public void Load()
    {
        SceneManager.LoadScene
        (
            PlayerPrefs.GetInt("SavedScene")
        );
    }
}