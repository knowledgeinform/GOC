using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    // instance variables
    private static DialogueManager dialogueInst;

    // animator
    public Animator DialogueAnimator;

    // dialogue UI variables
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBoxUI;
    public bool isSystemText { private get; set; }
    private GameObject dialogueType; // NPC or system dialogue box
    private GameObject dialogueBox;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;
    private GameObject continueButton;
    private GameObject choicesText;
    private GameObject dialogueImage;
    private Sprite targetImage;
    
    // choice variables
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesTextArray;
    
    // audio variables
    private AudioSource audioSource;
    private AudioClip[] audioClips;
    private AudioClip textScroll;
    private string audioClipFolder;

    // dialogue-related variables
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private string playerName = "Astro"; // TODO: replace this with persistent variable
    private string sceneName;
    private bool goToFinal = false;

    // ship colors
    // [Header("Ship Color Change")]
    // [SerializeField] private Material[] _mats;
    // [SerializeField] private GameObject _ship;
    // private Renderer _rendererShip;

    // tags (e.g., #speaker:SYS)
    private const string SPEAKER_TAG = "speaker";
    private const string SCENE_TAG = "scene";
    private const string TOGGLE_IMAGE_ON_TAG = "toggleImageOn";
    private const string IMAGE_TAG = "image";
    private const string SHIP_COLOR_TAG = "shipColor";

    // Start is called before the first frame update
    void Start()
    {
        // _rendererShip = _ship.AddComponent<Renderer>();
        // _rendererShip.enabled = false;

        InitializeDialogueBox();

        audioSource = dialogueBox.AddComponent<AudioSource>();

        dialogueIsPlaying = false;
        dialogueBoxUI.SetActive(false);
        dialogueImage.SetActive(false);

        ContinueButtonActive(false);
        choicesText.SetActive(false);

        // get all of the choices text
        choicesTextArray = new TextMeshProUGUI[choices.Length];
        int i = 0;
        foreach (GameObject c in choices)
        {
            choicesTextArray[i] = c.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    private void InitializeDialogueBox()
    {
        // this if statement doesn't work; TODO: find solution
        if (isSystemText)
        {
            // initialize to standard NPC box for now, switch to system box if needed later
            dialogueType = dialogueBoxUI.transform.GetChild(1).gameObject;

            // hide the other box
            dialogueBoxUI.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            // initialize to standard NPC box for now, switch to system box if needed later
            dialogueType = dialogueBoxUI.transform.GetChild(0).gameObject;

            // hide the other box
            dialogueBoxUI.transform.GetChild(1).gameObject.SetActive(false);
        }

        // set all the UI elements
        dialogueBox = dialogueType.transform.GetChild(0).gameObject;
        nameText = dialogueType.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        dialogueText = dialogueType.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        continueButton = dialogueType.transform.GetChild(3).gameObject;
        choicesText = dialogueType.transform.GetChild(4).gameObject;
        dialogueImage = dialogueType.transform.GetChild(5).gameObject;
    }

    private void Update()
    {
        // return right away if dialogue is playing
        if (!dialogueIsPlaying)
        {
            audioSource.Stop();
            return;
        }
    }

    private void Awake()
    {
        if (dialogueInst != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in this scene!");
        }
        dialogueInst = this;
    }

    public static DialogueManager GetInstance()
    {
        return dialogueInst;
    }

    private void CreateAudioClips()
    {
        audioClipFolder = "Audio/" + nameText.text + "AudioSFX";
        Debug.Log(audioClipFolder);
        audioClips = Resources.LoadAll<AudioClip>(audioClipFolder);
        textScroll = Resources.Load<AudioClip>("Audio/UI_SFX/TextScroll"); // universal text sfx
        Debug.Log(audioClipFolder);
    }

    private string InsertPlayerNameInString(string s)
    {
        return s.Replace("<&player>", playerName);
    }

    private void ContinueButtonActive(bool b)
    {
        continueButton.gameObject.SetActive(b);
    }

    private IEnumerator WaitUntilAnimation(string animator)
    {
        DialogueAnimator.SetTrigger(animator);
        yield return new WaitForSeconds(0.5f);
        // do
        // {
        //     Debug.Log(animator + " is playing: " + DialogueAnimator.GetBool(animator));
        //     yield return null;
        // } while (DialogueAnimator.GetBool(animator));
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueBoxUI.SetActive(true);
        sceneName = "";
        nameText.text = "???";
        
        // StartCoroutine(WaitUntilAnimation("Enter"));
        StopAllCoroutines();

        // continue each line of dialogue
        StartCoroutine(DisplayNextSentence());
    }

    public void ContinueStory()
    {
        StartCoroutine(DisplayNextSentence());
    }

    private IEnumerator DisplayNextSentence()
    {
        if (currentStory.canContinue)
        {
            // hide the continue text while dialogue is printing to the panel
            ContinueButtonActive(false);

            if (goToFinal)
            {
                // currentStory.ChoosePathString("finalMessage");
            }

            // set dialogue text to the current chunk of text and type
            dialogueText.text = InsertPlayerNameInString(currentStory.Continue());
            HandleTags(currentStory.currentTags);

            // check if the message should go to the final knot in the story
            if ((string) currentStory.variablesState["goToFinal"] == "true")
            {
                goToFinal = true;
            }

            yield return StartCoroutine(TypeSentence(dialogueText.text));
            
            // show the continue button for the next round of dialogue
            ContinueButtonActive(true);

            // display the choices (if any)
            yield return StartCoroutine(DisplayChoices());
        }
        else
        {
            StopDialogue();

            // check for any scene switches
            if (sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    private void StopDialogue()
    {
        // StartCoroutine(WaitUntilAnimation("Exit"));

        dialogueIsPlaying = false;
        dialogueBoxUI.SetActive(false);
        ContinueButtonActive(false);
        dialogueText.text = "";
    }

    private IEnumerator TypeSentence(string sentence)
    {
        // go through each letter in a given sentence(s) and play a sound effect along with it
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            // if Z or space are inputted, skip to the next chunk of dialogue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Skipped dialogue!");
                dialogueText.text = sentence;
                break;
            }
            dialogueText.text += c;
            PlayLetter(c);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void PlayLetter(char c)
    {
        if (c != 32)
        {
            audioSource.PlayOneShot(textScroll, 0.5f);
        }

        // play random sounds from the AudioClip array
        if (audioClips.Length != 0)
        {
            // audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)], 0.5f);
        }
    }

    private IEnumerator DisplayChoices()
    {
        choicesText.SetActive(true);
        List<Choice> currentChoices = currentStory.currentChoices;

        // first check if the UI can support the incoming number of choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than what can be supported. Number of choices given: "
                + currentChoices.Count);
        }

        // enable and initialize choices for the amount of choices provided
        int index = 0;
        foreach (Choice c in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesTextArray[index].text = c.text;
            index++;
        }

        // go through the remaining choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        if (currentChoices.Count > 0)
        {
            ContinueButtonActive(false);
        }

        yield return StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    
    public void MakeChoice(int choiceIndex)
    {
        StartCoroutine(SelectChoice(choiceIndex));
    }

    private IEnumerator SelectChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(DisplayNextSentence());
        choicesText.SetActive(false);
    }

    private void HandleTags(List<string> tags)
    {
        // loop through and parse each tag
        foreach (string t in tags)
        {
            string[] splitTag = t.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tags);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // TODO: add switch statement to handle different tagKey values (portraits, NPC vs. system, etc.)

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (tagValue == "SYS")
                    {
                        nameText.text = "";
                    }
                    else
                    {
                        nameText.text = tagValue;
                    }
                    CreateAudioClips();
                    break;
                    
                case SCENE_TAG:
                    sceneName = tagValue;
                    break;

                case TOGGLE_IMAGE_ON_TAG:
                    if (tagValue == "ON")
                    {
                        dialogueImage.SetActive(true);
                    }
                    else if (tagValue == "OFF")
                    {
                        dialogueImage.SetActive(false);
                    }
                    break;

                case SHIP_COLOR_TAG:
                    TextMeshPro colorChange = GameObject.Find("ShipColorChange").GetComponent<TextMeshPro>();

                    if (colorChange == null)
                    {
                        Debug.LogError("Ship color cannot be changed to this color: " + colorChange.text);
                    }
                    else
                    {
                        colorChange.text = tagValue;
                    }

                    break;

                case IMAGE_TAG:
                    string imageFrom = "DialogueResourceImages/" + tagValue;
                    targetImage = Resources.Load<Sprite>(imageFrom);
                    dialogueImage.GetComponent<Image>().sprite = targetImage;
                    break;
            }
        }
    }
}
