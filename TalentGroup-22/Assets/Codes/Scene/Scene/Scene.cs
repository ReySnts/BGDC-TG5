using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
    protected List<GameObject> gameObjects = new List<GameObject>();
    protected virtual void RegisterGameObject(){}
    protected virtual void DisableCertainConditions(){}
    public virtual void EnableAllGameObject()
    {
        foreach (GameObject gameObj in gameObjects) gameObj.SetActive(true);
    }
    public void DisableAllGameObject()
    {
        foreach (GameObject gameObj in gameObjects) gameObj.SetActive(false);
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