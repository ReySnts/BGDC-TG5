using System.Collections;
using UnityEngine;
public class SceneLevel_1 : Scene
{
    public static SceneLevel_1 objInstance = null;
    GameObject controlScreen = null;
    GameObject introScreen = null;
    float waitTime = 4f;
    public override void EnableAllGameObject()
    {
        PlayerMovement.objInstance.enabled = true;
        PlayerHealth.objInstance.enabled = true;
        PlayerHide.objInstance.enabled = true;
        Puzzle.objInstance.enabled = true;
        Score.objInstance.enabled = true;
    }
    public override void DisableAllGameObject()
    {
        PlayerMovement.objInstance.enabled = false;
        PlayerHealth.objInstance.enabled = false;
        PlayerHide.objInstance.enabled = false;
        Puzzle.objInstance.enabled = false;
        Score.objInstance.enabled = false;
    }
    IEnumerator DisableFrameAfterAwake()
    {
        yield return new WaitForEndOfFrame();
        DisableAllGameObject();
        PauseMenu.objInstance.DisableAllMenu();
    }
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        UnfreezeTime();
        StartCoroutine
        (
            DisableFrameAfterAwake()
        );
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
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}