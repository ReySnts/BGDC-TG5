using System.Collections.Generic;
using UnityEngine;
public abstract class Menu : MonoBehaviour
{
    protected List<GameObject> menus = new List<GameObject>();
    public abstract void RegisterMenu();
    public void EnableAllMenu()
    {
        foreach (GameObject menu in menus) menu.SetActive(true);
    }
    public void DisableAllMenu()
    {
        foreach (GameObject menu in menus) menu.SetActive(false);
    }
}