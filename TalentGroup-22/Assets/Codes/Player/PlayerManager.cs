using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    int score;
    float pX, pY;
    public GameObject player;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            pX = player.transform.position.x;
            pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            score = PlayerPrefs.GetInt("SavedScore");

            player.transform.position = new Vector2(pX, pY);
            PlayerPrefs.SetInt("TimeToLoad", 0);
            PlayerPrefs.Save();
        }
    }


    public void SaveData()
    {
        pX = player.transform.position.x;
        pY = player.transform.position.y;

        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);
        PlayerPrefs.SetInt("SavedScore", score);

        Debug.Log("Score : " + PlayerPrefs.GetInt("SavedScore"));
        
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("p_x");
        PlayerPrefs.DeleteKey("p_y");
        PlayerPrefs.DeleteKey("TimeToLoad");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("SavedScore");
    }

}
