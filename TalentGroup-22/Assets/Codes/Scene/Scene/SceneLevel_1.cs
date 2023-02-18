using System.Collections;
using UnityEngine;
public class SceneLevel_1 : Scene
{
    public static SceneLevel_1 objInstance = null;
    GameObject introScreen = null;
    void DisableCertainConditions(GameObject gameObject)
    {
        if (gameObject == Puzzle.objInstance.uIGameObj) Puzzle.objInstance.Activate();
    }
    public override void EnableAllGameObject()
    {
        foreach (GameObject gameObj in gameObjects) 
        {
            gameObj.SetActive(true);
            DisableCertainConditions(gameObj);
        }
    }
    IEnumerator DisableFrameAfterAwake()
    {
        yield return new WaitForEndOfFrame();
        DisableAllGameObject();
        PauseMenu.objInstance.DisableAllMenu();
    }
    protected override void RegisterGameObject()
    {
        #region List Singletons
        gameObjects.Add
        (
            GameObject.Find("PlayerMovement")  
        );
        gameObjects.Add
        (
            GameObject.Find("PlayerHealth")  
        );
        gameObjects.Add
        (
            GameObject.Find("PlayerHide")  
        );
        gameObjects.Add
        (
            GameObject.Find("Score")  
        );
        gameObjects.Add
        (
            GameObject.Find("Puzzle")  
        );
        #endregion
        #region List UIs
        gameObjects.Add
        (
            GameObject.Find("ScoreUI")
        );
        gameObjects.Add
        (
            GameObject.Find("Blood")
        );
        gameObjects.Add
        (
            GameObject.Find("PuzzleUI")  
        );
        #endregion
        StartCoroutine
        (
            DisableFrameAfterAwake()
        );
    }
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        UnfreezeTime();
        RegisterGameObject();
    }
    void FadeIntroScreen()
    {
        EnableAllGameObject();
        PauseMenu.objInstance.canBeAccessed = true;
        introScreen.SetActive(false);
    }
    IEnumerator WaitToFade()
    {
        yield return new WaitForSeconds(4f);
        FadeIntroScreen();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        introScreen = GameObject.Find("Intro");
        introScreen.SetActive(true);
        StartCoroutine(
            WaitToFade()
        );
    }
}