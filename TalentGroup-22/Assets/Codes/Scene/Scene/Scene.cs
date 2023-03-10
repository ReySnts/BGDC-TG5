using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene : MonoBehaviour
{
    protected List<GameObject> gameObjects = new List<GameObject>();
    protected void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }
    public virtual void EnableAllGameObject(){}
    public virtual void DisableAllGameObject(){}
    public void Restart()
    {
        #region Delete Keys
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("TimeToLoad");
        #endregion
        ShardData.objInstance.ResetAll();
        SceneManager.LoadScene
        (
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void NextScene()
    {
        SceneManager.LoadScene
        (
            SceneManager.GetActiveScene().buildIndex + 1
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