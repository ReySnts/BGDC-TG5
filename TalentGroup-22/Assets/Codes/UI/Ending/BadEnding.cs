using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadEnding : MonoBehaviour
{
    public GameObject buttonPanel;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        buttonPanel.gameObject.SetActive(false);
        anim.SetTrigger("play");
        StartCoroutine(DisplayButton(anim.GetCurrentAnimatorStateInfo(0).length));

    }

    IEnumerator DisplayButton(float delay = 0.1f)
    {
        yield return new WaitForSeconds(delay);
        buttonPanel.gameObject.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void RestartButton()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("Saved");
        PlayerPrefs.DeleteKey("TimeToLoad");
        SceneManager.LoadScene
        (
            "Level-1"
        );
    }
}