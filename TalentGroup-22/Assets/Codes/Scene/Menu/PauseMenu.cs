using UnityEngine;
public class PauseMenu : Menu
{
    public bool hasPaused = false;
    string pauseMenu = "PauseMenu";
    string settingInGame = "SettingInGame";
    public void Resume()
    {
        hasPaused = false;
        DisableAllMenu();
        Time.timeScale = 1f;
    }
    public override void RegisterMenu()
    {
        menus.Add
        (
            GameObject.Find(pauseMenu)
        );
        menus.Add
        (
            GameObject.Find(settingInGame)
        );
    }
    public void OpenPause()
    {
        foreach (GameObject menu in menus) 
        {
            if (menu.name == pauseMenu) menu.SetActive(true);
            else menu.SetActive(false);
        }
    }
    public void Pause()
    {
        hasPaused = true;
        OpenPause();
        Time.timeScale = 0f;
    }
    void Update()
    {
        if 
        (
            Input.GetKeyDown(KeyCode.Escape)
        )
        {
            if (!hasPaused) Pause();
            else Resume();
        }
    }
    public void OpenSetting()
    {
        foreach (GameObject menu in menus) 
        {
            if (menu.name == settingInGame) menu.SetActive(true);
            else menu.SetActive(false);
        }
    }
    public void Restart()
    {
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("TimeToLoad");
        Scene.objInstance.Restart();
    }
}