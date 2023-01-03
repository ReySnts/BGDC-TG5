using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    const string ShardData = "WasDestroyed";
    public void NewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetString(ShardData, "true");
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }


    public void QuitGame()
    {
        Application.Quit();

    }

}
