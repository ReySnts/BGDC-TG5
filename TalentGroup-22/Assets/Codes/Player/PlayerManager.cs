using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    float pX, pY;
    public GameObject player;

    private void Start()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        if (PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            pX = player.transform.position.x;
            pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            
            player.transform.position = new Vector2(pX, pY);
            PlayerPrefs.SetInt("TimeToLoad", 0);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("SavedScene") == SceneManager.GetActiveScene().buildIndex)
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        pX = player.transform.position.x;
        pY = player.transform.position.y;

        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);
        
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }


}
