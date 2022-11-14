using UnityEngine;
public class Data : MonoBehaviour
{
    public static Data objInstance = null;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
}