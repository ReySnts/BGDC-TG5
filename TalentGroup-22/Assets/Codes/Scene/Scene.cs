using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<<< HEAD:TalentGroup-22/Assets/Codes/Scene/SceneCodes.cs
public class SceneCodes : MonoBehaviour
{
    public static SceneCodes objInstance = null;
========
public class Scene : MonoBehaviour
{
    public static Scene objInstance = null;
>>>>>>>> rey:TalentGroup-22/Assets/Codes/Scene/Scene.cs
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