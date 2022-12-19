using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputName : MonoBehaviour
{
    public TMP_InputField nameField;

    void SaveName()
    {
        PlayerPrefs.SetString("SavedName", nameField.text);
        Debug.Log("save called as " + PlayerPrefs.GetString("SavedName"));
        PlayerPrefs.Save();
    }
}

