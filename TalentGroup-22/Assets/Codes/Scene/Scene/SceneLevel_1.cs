using System.Collections;
using UnityEngine;
public class SceneLevel_1 : Scene
{
    public static SceneLevel_1 objInstance = null;
    GameObject controlScreen = null;
    GameObject introScreen = null;
    float waitTime = 4f;
    protected override void DisableCertainConditions(GameObject gameObject)
    {
        if (gameObject == Puzzle.objInstance.uIGameObj) Puzzle.objInstance.Activate();
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
    IEnumerator WaitLevel()
    {
        yield return new WaitForSeconds(waitTime);
        EnableAllGameObject();
        PauseMenu.objInstance.canBeAccessed = true;
        introScreen.SetActive(false);
    }
    IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(waitTime);
        controlScreen.SetActive(false);
        introScreen.SetActive(true);
        StartCoroutine(
            WaitLevel()
        );
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controlScreen = GameObject.Find("Control");
        introScreen = GameObject.Find("Intro");
        introScreen.SetActive(false);
        controlScreen.SetActive(true);
        StartCoroutine
        (
            WaitIntro()
        );
    }
}