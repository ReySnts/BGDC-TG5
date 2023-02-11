using UnityEngine;
public class SceneCredits : Scene
{
    public static SceneCredits objInstance = null;
    void Awake()
    {
        objInstance ??= this;
        if (objInstance != this) Destroy(gameObject);
    }
}