using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueChoices : MonoBehaviour
{
    [SerializeField]
    private TextAsset _InkJsonFile;
    private Story _StoryScript;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;
    public TextMeshProUGUI displayDialog;

    public TextSegments[] textSegment;

    public Image characterIcon;
    public Image realMom;

    [SerializeField]
    private GridLayoutGroup choiceHolder;

    [SerializeField]
    private Button choiceBasePrefab;

    private float typingSpeed = 0.04f;
    private float textSpeed = 25f;
    private bool canContinueToNextLine = false;
    public Button continueButton;
    void Start()
    {
        LoadStory();
        realMom.gameObject.SetActive(false);
        displayDialog.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canContinueToNextLine)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    void LoadStory()
    {
        _StoryScript = new Story(_InkJsonFile.text);

        _StoryScript.BindExternalFunction("Name", (string charName) => ChangeName(charName));
        _StoryScript.BindExternalFunction("Icon", (string charName) => ChangeCharacterIcon(charName));

        DisplayNextLine();

    }

    public void DisplayNextLine()
    {
        continueButton.gameObject.SetActive(false);
        if (_StoryScript.canContinue) // Checking if there is content to go through
        {
            string text = _StoryScript.Continue(); //Gets Next Line
            text = text?.Trim(); //Removes White space from the text
            dialogueBox.text = text; //Displays new text
            StartCoroutine(PlayDialogue(dialogueBox.text = text));
            
        }
        else if (_StoryScript.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            SceneManager.LoadScene("GoodEnding");
        }
    }

    IEnumerator PlayDialogue(string line)
    {
        dialogueBox.text = "";

        canContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        canContinueToNextLine = true;
    }

    private void DisplayChoices()
    {
        if (choiceHolder.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _StoryScript.currentChoices.Count; i++)
        {
            var choice = _StoryScript.currentChoices[i];
            var button = CreateChoiceButton(choice.text); //creates a choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    Button CreateChoiceButton(string text)
    {
        //instantiate the button prefab
        var choiceButton = Instantiate(choiceBasePrefab);
        choiceButton.transform.SetParent(choiceHolder.transform, false);

        //change the text in the button prefab
        var ButtonText = choiceButton.GetComponentInChildren<TMP_Text>();
        ButtonText.text = text;

        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice)
    {
        if (canContinueToNextLine)
        {
            _StoryScript.ChooseChoiceIndex(choice.index);
            RefreshChoiceView();
            DisplayNextLine();

            if (choice.index == 0)//YES CHOICE
            {
                canContinueToNextLine = false;
                realMom.gameObject.SetActive(true);
                displayDialog.gameObject.SetActive(true);
                StartCoroutine(BadEndDialog(textSegment[0].Dialogue));
            }
        }
        


    }

    void RefreshChoiceView()
    {
        if (choiceHolder != null)
        {
            foreach(var button in choiceHolder.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }

    public void ChangeName(string name)
    {
        string SpeakerName = name;

        nameTag.text = SpeakerName;
    }

    public void ChangeCharacterIcon(string charName)
    {
        characterIcon.sprite = Resources.Load<Sprite>("CharacterIcons/" + charName);
    }

    IEnumerator BadEndDialog(string Dialogue)
    {
        displayDialog.SetText(string.Empty);
        for (int i = 0; i < Dialogue.Length; i++)
        {
            displayDialog.text += Dialogue[i];
            yield return new WaitForSeconds(1 / textSpeed);
        }
        StartCoroutine(BadEndScene());
    }
    IEnumerator BadEndScene()
    {
        yield return new WaitForSeconds(10/textSpeed);
        SceneManager.LoadScene("BadEnding");
    }
}

[System.Serializable]
public class TextSegments
{
    public string Dialogue;
}
