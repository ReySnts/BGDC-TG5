using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
    public static Scene objInstance = null;
    protected List<GameObject> gameObjects = new List<GameObject>();
    protected virtual void RegisterGameObject(){}
    protected void Awake()
    {
        objInstance ??= this;
        if (objInstance != gameObject) Destroy(gameObject);
        else RegisterGameObject();
    }
    protected void DisableAllGameObject()
    {
        foreach (GameObject gameObj in gameObjects) gameObj.SetActive(false);
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
    public void Restart()
    {
        SceneManager.LoadScene
        (
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ViewCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}