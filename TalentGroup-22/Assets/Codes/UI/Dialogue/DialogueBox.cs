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
    [Space]
    public TextMeshProUGUI SpeakerNameDisplay;
    public TextMeshProUGUI DialogueDisplay;
    [Space]
    public float TextSpeed;
    public TMP_InputField nameField;

    private int DialogueIndex;
    private bool canContinueToNextLine = false;

    // Start is called before the first frame update
    void Start()
    {
        SetStyle(DialogueSegments[0].Speaker);
        StartCoroutine(PlayDialogue(DialogueSegments[0].Dialogue));
        nameField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (canContinueToNextLine && Input.GetKeyDown(KeyCode.Return))
        {
            DialogueIndex++;
            nameField.gameObject.SetActive(true);
           PlayerPrefs.SetString("SavedName", nameField.text);
            if (DialogueIndex == DialogueSegments.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
            SetStyle(DialogueSegments[DialogueIndex].Speaker);
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex].Dialogue.Replace("playerName", (PlayerPrefs.GetString("SavedName")))));
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

        canContinueToNextLine = false;

        for (int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            yield return new WaitForSeconds(1f / TextSpeed);
        }

        canContinueToNextLine = true;
    } 

}

    [System.Serializable]
    public class DialogueSegment
    {
        public string Dialogue;
        public Subject Speaker;
    } 
