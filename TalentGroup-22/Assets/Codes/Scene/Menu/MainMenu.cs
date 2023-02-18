using UnityEngine;
public class MainMenu : Menu
{
    public static MainMenu objInstance = null;
    const string ShardData = "WasDestroyed";
    readonly string mainMenu = "MainMenu";
    readonly string settingsMenu = "SettingsMenu";
    public override void RegisterMenu()
    {
        menus.Add
        (
            GameObject.Find(mainMenu)
        );
        menus.Add
        (
            GameObject.Find(settingsMenu)
        );
    }
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        RegisterMenu();
    }
    public void OpenMenu()
    {
        foreach (GameObject menu in menus) 
        {
            if (menu.name == mainMenu) menu.SetActive(true);
            else menu.SetActive(false);
        }
    }
    void Start()
    {
        OpenMenu();
    }
    public void NewGame() 
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("TimeToLoad"); 
        SceneMenu.objInstance.StartNewGame();
    }
    public void LoadGame()
    {
        try
        {
            PlayerPrefs.SetString
            (
                ShardData, 
                "true"
            );
            SceneMenu.objInstance.Load();
        }
        catch{}
    }
    public void OpenSettings()
    {
        foreach (GameObject menu in menus) 
        {
            if (menu.name == settingsMenu) menu.SetActive(true);
            else menu.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}