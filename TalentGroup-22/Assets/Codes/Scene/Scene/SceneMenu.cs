using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMenu : Scene
{
    public static SceneMenu objInstance = null;
    void Awake()
    {
        objInstance ??= this;
        if (objInstance != this) Destroy(gameObject);
        else RegisterGameObject();
    }
    public void StartNewGame()
    {
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