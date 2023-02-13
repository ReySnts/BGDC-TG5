using System.Collections;
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
    public Button continueButton;
    private bool canContinueToNextLine = false;
    IEnumerator PlayDialogue(string Dialogue)
    {
        DialogueDisplay.SetText(string.Empty);
        canContinueToNextLine = false;
        for (int i = 0; i < Dialogue.Length; i++)
        {
            DialogueDisplay.text += Dialogue[i];
            yield return new WaitForSeconds(TextSpeed);
        }
        canContinueToNextLine = true;
    }
    void Start()
    {
        TextSpeed = Time.deltaTime;
        SetStyle(DialogueSegments[0].Speaker);
        StartCoroutine
        (
            PlayDialogue(DialogueSegments[0].Dialogue)
        );
        nameField.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (DialogueIndex == 3) 
        {
            if 
            (
                nameField.text == null 
                || 
                nameField.text.Length <= 0 
                || 
                nameField.text == ""
            ) 
            canContinueToNextLine = false;
            else canContinueToNextLine = true;
        }
        continueButton.gameObject.SetActive(canContinueToNextLine);
    }
    public void ContinueSentences()
    {
        continueButton.gameObject.SetActive(false);
        DialogueIndex++;
        if (DialogueIndex == DialogueSegments.Length)
        {
            SceneManager.LoadScene
            (
                SceneManager.GetActiveScene().buildIndex + 1
            );
            return;
        }
        else if (DialogueIndex == 3) nameField.gameObject.SetActive(true);
        else if (DialogueIndex == 4)
        {
            PlayerPrefs.SetString
            (
                "SavedName", 
                nameField.text
            );
            nameField.gameObject.SetActive(false);
        }
        SetStyle
        (
            DialogueSegments[DialogueIndex].Speaker
        );
        StartCoroutine
        (
            PlayDialogue
            (
                DialogueSegments[DialogueIndex].Dialogue.Replace
                (
                    "playerName", 
                    PlayerPrefs.GetString("SavedName")
                )
            )
        );
    }
    void SetStyle(Subject Speaker)
    {
        if (Speaker.SubjectFace == null) SpeakerFaceDisplay.color = new Color
        (
            0f, 
            0f, 
            0f, 
            0f
        );
        else
        {
            SpeakerFaceDisplay.sprite = Speaker.SubjectFace;
            SpeakerFaceDisplay.color = Color.white;
        }
        SpeakerNameDisplay.SetText(Speaker.SubjectName);
    }
}
[System.Serializable]
public class DialogueSegment
{
    public string Dialogue;
    public Subject Speaker;
}