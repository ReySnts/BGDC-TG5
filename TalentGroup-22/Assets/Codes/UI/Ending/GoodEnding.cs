using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoodEnding : MonoBehaviour
{
    public GameObject buttonPanel;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        buttonPanel.gameObject.SetActive(false);
        StartCoroutine(DisplayButton(anim.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator DisplayButton(float delay = 5.0f)
    {
        yield return new WaitForSeconds(delay);

        anim.SetTrigger("play");
        buttonPanel.gameObject.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
