using System.Collections;
using UnityEngine;
public class Credits : MonoBehaviour
{
    Animator animator = null;
    float transitionTime = 20f;
    IEnumerator LoadCredits()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Scene.objInstance.ExitToMainMenu();
    }
    void Start()
    {
        Time.timeScale = 1f;
        animator = GameObject.Find("CreditsAnimator").GetComponent<Animator>();
        StartCoroutine
        (
            LoadCredits()
        );
    }
}