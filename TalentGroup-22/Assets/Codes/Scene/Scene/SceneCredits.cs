using System.Collections;
using UnityEngine;
public class SceneCredits : Scene
{
    public static SceneCredits objInstance = null;
    Animator animator = null;
    float transitionTime = 20f;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        UnfreezeTime();
    }
    IEnumerator LoadCredits()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneCredits.objInstance.ExitToMainMenu();
    }
    void Start()
    {
        animator = GameObject.Find("CreditsAnimator").GetComponent<Animator>();
        StartCoroutine
        (
            LoadCredits()
        );
    }
}