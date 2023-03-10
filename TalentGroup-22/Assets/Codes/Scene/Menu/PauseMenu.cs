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
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        RegisterMenu();
    }
    public void OpenPause()
    {
        foreach (GameObject menu in menus) 
        {
            if (menu.name == pauseMenu) menu.SetActive(true);
            else menu.SetActive(false);
        }
    }
    void Activate()
    {
        Cursor.lockState = hasPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = hasPaused ? 0f : 1f;
    }
    public void Pause()
    {
        hasPaused = true;
        SceneLevel_1.objInstance.DisableAllGameObject();
        OpenPause();
        Activate();
    }
    public void Resume()
    {
        hasPaused = false;
        DisableAllMenu();
        Activate();
        SceneLevel_1.objInstance.EnableAllGameObject();
        Puzzle.objInstance.Activate();
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
}