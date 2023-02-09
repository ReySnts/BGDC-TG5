using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCodes : MonoBehaviour
{
    public static SceneCodes objInstance = null;

    int sceneIdx = 0;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    public void Restart()
    {
        sceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIdx);
    }
}