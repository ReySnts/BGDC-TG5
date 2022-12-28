using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueBox : MonoBehaviour
{
    public DialogueSegment[] DialogueSegments;
    [Space]
    public Image SpeakerFaceDisplay;
    public Image DialogueBoxBorder;
    public Image DialogueBoxInner;
 //   public Image SkipIndicator;
    [Space]
    public TextMeshProUGUI SpeakerNameDisplay;
    public TextMeshProUGUI DialogueDisplay;
    [Space]
    public float TextSpeed;
    public TMP_InputField nameField;

    private int DialogueIndex;
  //  private bool CanContinue;

    // Start is called before the first frame update
    void Start()
    {
        SetStyle(DialogueSegments[0].Speaker);
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        
    }

    // Update is called once per frame
    void Update()
    {
        //    SkipIndicator.enabled = CanContinue; && CanContinue
        if (Input.GetKeyDown(KeyCode.Space) )
        { 
            DialogueIndex++;
            if (DialogueIndex == DialogueSegments.Length)
            {
                SceneManager.LoadScene("Level-1"); 
                //gameObject.SetActive(false);
               
                return;
                
            }
            PlayerPrefs.SetString("SavedName", nameField.text);
            SetStyle(DialogueSegments[DialogueIndex].Speaker);
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue.Replace("playerName", (PlayerPrefs.GetString("SavedName")))));
        }
        
        if (PlayerPrefs.GetString("SavedName") ==  nameField.text)
        {
            PlayerPrefs.SetInt("SavedScore", 0);
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
        DialogueDisplay.SetText(string.Empty);

        for(int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            yield return new WaitForSeconds(1f / TextSpeed);
        }
    }



}

    [System.Serializable]
    public class DialogueSegment
    {
        public string Dialogue;
        public Subject Speaker;
    }