using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void NewGame() 
    {
        DeleteKey();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {

        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }


    public void QuitGame()
    {
        Application.Quit();

    }

    public void DeleteKey()
    {
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("playerName");
        PlayerPrefs.DeleteAll();
    }

}
