using System.Collections.Generic;
using UnityEngine;
public abstract class Menu : MonoBehaviour
{
    public static Menu objInstance = null;
    protected List<GameObject> menus = new List<GameObject>();
    public abstract void RegisterMenu();
    protected void Awake()
    {
        objInstance ??= this;
        if (objInstance != this) Destroy(gameObject);
        else RegisterMenu();
    }
    public virtual void DisableAllMenu()
    {
        foreach (GameObject menu in menus) menu.SetActive(false);
    }
}