using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    int score;
    float pX, pY;
    string playerName;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    float musicVolume, sfxVolume;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            pX = player.transform.position.x;
            pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");
            score = PlayerPrefs.GetInt("SavedScore");
            
            musicVolume = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY);
            sfxVolume = PlayerPrefs.GetFloat(AudioManager.SFX_KEY);
            playerName = PlayerPrefs.GetString("SavedName");

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
        PlayerPrefs.SetFloat("MusicPref", musicVolume);
        PlayerPrefs.SetFloat("SfxPref", sfxVolume);
        
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
        PlayerPrefs.DeleteKey("MusicPref");
        PlayerPrefs.DeleteKey("SfxPref");
        
    }

}
