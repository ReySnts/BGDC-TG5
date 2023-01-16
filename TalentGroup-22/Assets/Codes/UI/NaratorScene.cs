using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class NaratorScene : MonoBehaviour
{
    public NaratorSegment[] NaratorSegments;

    public TextMeshProUGUI DialogueDisplay;
    public float TextSpeed;

    private int DialogueIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(NaratorSegments[0].Dialogue)); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DialogueIndex++;
            if (DialogueIndex == NaratorSegments.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
            StartCoroutine(PlayDialogue(NaratorSegments[DialogueIndex].Dialogue));
        }

    }

    IEnumerator PlayDialogue(string Dialogue)
    {
        DialogueDisplay.SetText(string.Empty);

        for (int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            yield return new WaitForSeconds(1f / TextSpeed);
        }
    }
}

    [System.Serializable]
public class NaratorSegment
{
    public string Dialogue;
}