using UnityEngine;
public class PauseMenu : Menu
{
    public static PauseMenu objInstance = null;
    public bool canBeAccessed = false;
    bool hasPaused = false;
    readonly string pauseMenu = "PauseMenu";
    readonly string settingInGame = "SettingsMenu";
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
    void Awake()
    {
        objInstance ??= this;
        if (objInstance != this) Destroy(gameObject);
        else RegisterMenu();
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
        SceneLevel_1.objInstance.DisableAllGameObject();
        OpenPause();
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        hasPaused = false;
        DisableAllMenu();
        SceneLevel_1.objInstance.EnableAllGameObject();
        Time.timeScale = 1f;
    }
    void Update()
    {
        if 
        (
            canBeAccessed 
            && 
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
        SceneLevel_1.objInstance.Restart();
    }
}