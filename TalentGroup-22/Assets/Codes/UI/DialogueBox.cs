using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public Image SpeakerFaceDisplay;
    public Image DialogueBoxBorder;
    public Image DialogueBoxInner;
    public Image SkipIndicator;
    [Space]
    public TextMeshProUGUI SpeakerNameDisplay;
    public TextMeshProUGUI DialogueDisplay;
    [Space]
    public float TextSpeed;

    private int DialogueIndex;
    private bool CanContinue;

    // Start is called before the first frame update
    void Start()
    {
        SetStyle(DialogueSegments[0].Speaker);
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
    }

    // Update is called once per frame
    void Update()
    {
        SkipIndicator.enabled = CanContinue;
        if(Input.GetKeyDown(KeyCode.Space) && CanContinue)
        {
            DialogueIndex++;
            if (DialogueIndex == DialogueSegments.Length)
            {
                gameObject.SetActive(false);
                return;
            }
        }
           
        
    }

    void SetStyle(Subject Speaker)
    {
        if(Speaker.SubjectFace == null)
        {
            SpeakerFaceDisplay.color = new Color(0, 0, 0, 0);
        } else
        {
            SpeakerFaceDisplay.sprite = Speaker.SubjectFace;
            SpeakerFaceDisplay.color = Color.white;
        }

        
        SpeakerNameDisplay.SetText(Speaker.SubjectName);
            
    }

    IEnumerator PlayDialogue(string Dialogue)
    {

        CanContinue = false;
        DialogueDisplay.SetText(string.Empty);

        for(int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            yield return new WaitForSeconds(1f / TextSpeed);
        }

        CanContinue = true;
    }

}

[System.Serializable]
public class DialogueSegment
{
    public string Dialogue;
    public Subject Speaker;
}