using UnityEngine;
using UnityEngine.SceneManagement;
public class PuzzleData : MonoBehaviour
{
    public static PuzzleData objInstance = null;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
}