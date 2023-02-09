using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator TextAnim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(3);

        TextAnim.SetTrigger("fade");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
